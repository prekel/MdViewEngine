namespace rec MdViewEngine

open System

type Undefined = exn

type MdPiece =
    | Regular of string
    | Italic of string
    | Bold of string
    | BoldItalic of string
    | Mono of string

type MdPara = MdPara of MdPiece list

type MdLine =
    | Heading1 of MdPara
    | Heading2 of MdPara
    | Heading3 of MdPara
    | Heading4 of MdPara
    | Heading5 of MdPara
    | Heading6 of MdPara
    | Paragraph of MdPara
    | Blockquote of MdDocument
    | Code of string * string option

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

    let rec renderBlockquote (MdDocument md) depth =
        md
        |> List.map (fun line ->
            match line with
            | Blockquote q -> renderBlockquote q (depth + 1)
            | x ->
                let r: string = renderLine x
                let lines: string [] = r.Split '\n'
                let quoteSigns = String.init depth (fun _ -> ">")

                lines
                |> Array.map (fun line -> sprintf "%s %s" <|| (quoteSigns, line))
                |> String.concat "\n"

            )
        |> String.concat "\n"

    let renderLine (line: MdLine) =
        match line with
        | Heading1 para -> "# " + renderPara para
        | Heading2 para -> "## " + renderPara para
        | Heading3 para -> "### " + renderPara para
        | Heading4 para -> "#### " + renderPara para
        | Heading5 para -> "##### " + renderPara para
        | Heading6 para -> "###### " + renderPara para
        | Paragraph para -> renderPara para
        | Blockquote md -> renderBlockquote md 1
        | Code (code, lang) ->
            "```"
            + (lang |> Option.defaultValue "")
            + "\n"
            + code
            + "\n```"

    let render (MdDocument md): string =
        md |> List.map renderLine |> String.concat "\n\n"
