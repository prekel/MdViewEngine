namespace rec MdViewEngine

type Undefined = exn

type MdPiece =
    | Regular of string
    | Italic of string
    | Bold of string
    | BoldItalic of string
    | Mono of string

type MdPara = MdPara of MdPiece list

type MdListItem =
    | ListPara of MdPara
    | ListItem of MdPara * MdLine list
    | SubList of MdList

type ListOrder =
    | Ordered
    | Unordered

type MdList = MdList of ListOrder * MdListItem list

type MdBlockquote = MdBlockquote of MdLine list

type MdCode = MdCode of string * string option

type MdImage = MdImage of Undefined

type MdLine =
    | Heading1 of MdPara
    | Heading2 of MdPara
    | Heading3 of MdPara
    | Heading4 of MdPara
    | Heading5 of MdPara
    | Heading6 of MdPara
    | Paragraph of MdPara
    | Blockquote of MdBlockquote
    | Code of MdCode
    | List of MdList
    | Image of MdImage

type MdDocument = MdDocument of MdLine list

module Md =
    let renderPiece piece =
        match piece with
        | Regular text -> text
        | Italic text -> sprintf "*%s*" text
        | Bold text -> sprintf "**%s**" text
        | BoldItalic text -> sprintf "***%s***" text
        | Mono text -> sprintf "`%s`" text

    let renderPara (MdPara para) =
        para |> List.map renderPiece |> String.concat ""

    let renderBlockquote md =
        let rec renderBlockquoteRec (MdBlockquote md) depth =
            let quoteSigns = String.init depth (fun _ -> ">")
            let delimiter = sprintf "\n%s\n" quoteSigns

            md
            |> List.map (fun line ->
                match line with
                | Blockquote q -> renderBlockquoteRec q (depth + 1)
                | x ->
                    (renderLine x: string).Split '\n'
                    |> Array.map (fun line -> sprintf "%s %s" <|| (quoteSigns, line))
                    |> String.concat "\n")
            |> String.concat delimiter

        renderBlockquoteRec md 1

    let renderList list =
        let rec renderListRec (MdList (order, list)) depth =
            let listSign =
                match order with
                | Ordered -> "1."
                | Unordered -> "-"

            let renderListPara para indent =
                sprintf "%s%s %s" indent listSign (renderPara para)

            let listIndent = String.init depth (fun _ -> "    ")

            list
            |> List.collect (fun item ->
                match item with
                | SubList subList -> renderListRec subList (depth + 1)
                | ListPara para -> renderListPara para listIndent |> List.singleton
                | ListItem (para, lines) ->
                    renderListPara para listIndent
                    :: (lines
                        |> List.collect (fun x ->
                            (renderLine x: string).Split '\n'
                            |> Array.map (fun line -> sprintf "%s    %s" <|| (listIndent, line))
                            |> Array.toList)
                        |> String.concat "\n"
                        |> List.singleton))

        renderListRec list 0 |> String.concat "\n\n"

    let renderCode (MdCode (code, lang)) =
        sprintf "```%s\n%s\n```" (lang |> Option.defaultValue "") code

    let renderLine line =
        match line with
        | Heading1 para -> sprintf "# %s" <| renderPara para
        | Heading2 para -> sprintf "## %s" <| renderPara para
        | Heading3 para -> sprintf "### %s" <| renderPara para
        | Heading4 para -> sprintf "#### %s" <| renderPara para
        | Heading5 para -> sprintf "##### %s" <| renderPara para
        | Heading6 para -> sprintf "###### %s" <| renderPara para
        | Paragraph para -> renderPara para
        | Blockquote md -> renderBlockquote md
        | Code code -> renderCode code
        | List list -> renderList list
        | Image _ -> failwith "todo"

    let render (MdDocument md) =
        md |> List.map renderLine |> String.concat "\n\n"
