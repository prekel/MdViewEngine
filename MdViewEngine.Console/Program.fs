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
                         yield Paragraph <| MdPara [ Regular <| string i ]
                     yield
                         Blockquote
                         <| MdDocument [ yield Heading1 <| MdPara [ Regular "Цитатаqwe" ]
                                         yield
                                             Paragraph
                                             <| MdPara [ Regular "Цитата "
                                                         Bold "asdas" ]
                                         yield
                                             Blockquote
                                             <| MdDocument [ yield Heading1 <| MdPara [ Regular "Цитата Вложенная" ]
                                                             yield
                                                                 Paragraph
                                                                 <| MdPara [ Regular "Цитата "
                                                                             Bold "вложенная" ]
                                                             yield Heading6 <| MdPara [ Regular "Цитатаqwe6" ]
                                                             yield Code("sadsadsd\nasdas", None)
                                                             yield
                                                                 Paragraph
                                                                 <| MdPara [ Regular "Цитата "
                                                                             Bold "asdas" ]
                                                             yield
                                                                 Blockquote
                                                                 <| MdDocument [ yield
                                                                                     Heading1
                                                                                     <| MdPara [ Regular "Цитатаqwe" ]
                                                                                 yield
                                                                                     Paragraph
                                                                                     <| MdPara [ Regular "Цитата "
                                                                                                 Bold "asdas" ]
                                                                                 yield
                                                                                     Blockquote
                                                                                     <| MdDocument [ yield
                                                                                                         Heading1
                                                                                                         <| MdPara [ Regular
                                                                                                                         "Цитата Вложенная" ]
                                                                                                     yield
                                                                                                         Paragraph
                                                                                                         <| MdPara [ Regular
                                                                                                                         "Цитата "
                                                                                                                     Bold
                                                                                                                         "вложенная" ]
                                                                                                     yield
                                                                                                         Heading6
                                                                                                         <| MdPara [ Regular
                                                                                                                         "Цитатаqwe6" ]
                                                                                                     yield
                                                                                                         Code
                                                                                                             ("sadsadsd\nasdas",
                                                                                                              None)
                                                                                                     yield
                                                                                                         Paragraph
                                                                                                         <| MdPara [ Regular
                                                                                                                         "Цитата "
                                                                                                                     Bold
                                                                                                                         "asdas" ] ] ] ] ] ]

    printfn "%A" <| md
    printfn "%s" <| Md.render md

    0
