namespace MdViewEngine

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
    | Paragraph of MdPara
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

    let renderLine (line: MdLine) =
        match line with
        | Heading1 para -> "# " + renderPara para
        | Paragraph para -> renderPara para
        | Code (code, lang) -> "```" + lang.Value + "\n" + code + "\n```"

    let render (MdDocument md): string =
        md |> List.map renderLine |> String.concat "\n\n"
