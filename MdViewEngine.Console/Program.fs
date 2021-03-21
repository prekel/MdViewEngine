open MdViewEngine

[<EntryPoint>]
let main argv =
    let md =
        MdDocument [ yield Heading1 <| MdPara [ Regular "qwe" ]
                     yield
                         Paragraph
                         <| MdPara [ Regular "Ghbdtn "
                                     Bold "asdas" ]
                     yield Code("int a = 0;", Some "c")
                     for i in [ 1 .. 3 ] do
                         yield Paragraph <| MdPara [ Regular <| string i ] ]

    printfn "%A" <| md
    printfn "%s" <| Md.render md

    0
