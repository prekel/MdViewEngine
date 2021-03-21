namespace rec MdViewEngine

type MdPiece =
    | Regular of string
    | Italic of string
    | Bold of string
    | BoldItalic of string
    | Mono of string

type MdPara = MdPara of MdPiece list

type MdListItem =
    | SubList of MdListItem list
    | ListItem of MdPara

type ListOrder =
    interface
    end

type Ordered =
    inherit ListOrder

type Unordered =
    inherit ListOrder

type MdList<'ListOrder when 'ListOrder :> ListOrder> = MdList of MdListItem list

type MdBlockquote = MdBlockquote of MdLine list

type MdLine =
    | Heading1 of MdPara
    | Heading2 of MdPara
    | Heading3 of MdPara
    | Heading4 of MdPara
    | Heading5 of MdPara
    | Heading6 of MdPara
    | Paragraph of MdPara
    | Blockquote of MdBlockquote
    | Code of string * string option
    | OrderedList of MdList<Ordered>
    | UnorderedList of MdList<Unordered>

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
        let listSign =
            match box list with
            | :? (MdList<Ordered>) -> "1."
            | :? (MdList<Unordered>) -> "-"
            | _ -> failwith "(ノ•̀ o •́ )ノ ~ ┻━┻"

        let (MdList list) = list

        let renderListPara para indent =
            sprintf "%s%s %s" indent listSign (renderPara para)

        let rec renderListRec list depth =
            let listIndent = String.init depth (fun _ -> "    ")

            list
            |> List.collect (fun item ->
                match item with
                | SubList subList -> renderListRec subList (depth + 1)
                | ListItem para -> renderListPara para listIndent |> List.singleton)

        renderListRec list 0 |> String.concat "\n"

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
        | Code (code, lang) -> sprintf "```%s\n%s\n```" (lang |> Option.defaultValue "") code
        | OrderedList list -> renderList list
        | UnorderedList list -> renderList list

    let render (MdDocument md) =
        md |> List.map renderLine |> String.concat "\n\n"
