open MdViewEngine

[<EntryPoint>]
let main argv =
    let md =
        MdDocument [ yield Heading1 <| MdPara [ Regular "qwe" ]
                     yield
                         List
                         <| MdList
                             (Ordered,
                              [ ListPara <| MdPara [ Regular "qwe1" ]
                                ListPara <| MdPara [ Regular "qwe2" ]
                                SubList
                                <| MdList
                                    (Unordered,
                                     [ ListPara <| MdPara [ Regular "qwe1" ]
                                       ListPara <| MdPara [ Regular "qwe2" ]
                                       ListPara <| MdPara [ Regular "qwe3" ]
                                       ListItem
                                       <| (MdPara [ Regular "qwe4 with item" ],
                                           [ Paragraph <| MdPara [ Regular "item" ]
                                             Blockquote
                                             <| MdBlockquote [ Code <| MdCode("int code = 123;", Some "c") ] ]) ]) ])
                     yield
                         Paragraph
                         <| MdPara [ Regular "Ghbdtn "
                                     Bold "asdas" ]
                     yield Code <| MdCode("int a = 123;", Some "c")
                     for i in [ 1 .. 3 ] do
                         yield Paragraph <| MdPara [ Regular <| string i ]
                     yield
                         Blockquote
                         <| MdBlockquote [ yield Heading1 <| MdPara [ Regular "Цитатаqwe" ]
                                           yield
                                               Paragraph
                                               <| MdPara [ Regular "Цитата "
                                                           Bold "asdas" ]
                                           yield
                                               Blockquote
                                               <| MdBlockquote [ yield Heading1 <| MdPara [ Regular "Цитата Вложенная" ]
                                                                 yield
                                                                     Paragraph
                                                                     <| MdPara [ Regular "Цитата "
                                                                                 Bold "вложенная" ]
                                                                 yield Heading6 <| MdPara [ Regular "Цитатаqwe6" ]
                                                                 yield Code <| MdCode("asdasd\nasdas", None)
                                                                 yield
                                                                     Paragraph
                                                                     <| MdPara [ Regular "Цитата "
                                                                                 Bold "asdas" ]
                                                                 yield
                                                                     Blockquote
                                                                     <| MdBlockquote [ yield
                                                                                           Heading1
                                                                                           <| MdPara [ Regular
                                                                                                           "Цитатаqwe" ]
                                                                                       yield
                                                                                           Paragraph
                                                                                           <| MdPara [ Regular "Цитата "
                                                                                                       Bold "asdas" ]
                                                                                       yield
                                                                                           Blockquote
                                                                                           <| MdBlockquote [ yield
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
                                                                                                                 <| MdCode
                                                                                                                     ("asdasd\nasdas",
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
