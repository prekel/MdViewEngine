open MdViewEngine

[<EntryPoint>]
let main argv =
    let md =
        MdDocument [ yield Heading1 <| MdPara [ Regular "qwe" ]
                     yield
                         OrderedList
                         <| MdList [ ListItem <| MdPara [ Regular "qwe1" ]
                                     ListItem <| MdPara [ Regular "qwe2" ]
                                     ListItem <| MdPara [ Regular "qwe3" ]
                                     SubList [ ListItem <| MdPara [ Regular "asd1" ]
                                               ListItem <| MdPara [ Regular "asd2" ] ]
                                     ListItem <| MdPara [ Regular "zxc1" ]
                                     ListItem <| MdPara [ Regular "sadsafsd" ] ]

                     yield
                         UnorderedList
                         <| MdList [ ListItem
                                     <| MdPara [ Regular "qwe1 "
                                                 BoldItalic "asdsa" ]
                                     ListItem <| MdPara [ Regular "qwe2" ]
                                     ListItem <| MdPara [ Regular "qwe3" ]
                                     SubList [ ListItem <| MdPara [ Regular "asd1" ]
                                               ListItem <| MdPara [ Regular "asd2" ] ]
                                     ListItem <| MdPara [ Regular "zxc1" ]
                                     ListItem <| MdPara [ Regular "sadsafsd" ] ]
                     yield
                         Paragraph
                         <| MdPara [ Regular "Ghbdtn "
                                     Bold "asdas" ]
                     yield Code("int a = 0;", Some "c")
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
                                                                 yield Code("sadsadsd\nasdas", None)
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
                                                                                                                     ("let a = 42",
                                                                                                                      Some
                                                                                                                          "fs")
                                                                                                             yield
                                                                                                                 Paragraph
                                                                                                                 <| MdPara [ Regular
                                                                                                                                 "Цитата "
                                                                                                                             Bold
                                                                                                                                 "asdas" ]
                                                                                                             yield
                                                                                                                 OrderedList
                                                                                                                 <| MdList [ ListItem
                                                                                                                             <| MdPara [ Regular
                                                                                                                                             "qwe1" ]
                                                                                                                             ListItem
                                                                                                                             <| MdPara [ Regular
                                                                                                                                             "qwe2" ]
                                                                                                                             ListItem
                                                                                                                             <| MdPara [ Regular
                                                                                                                                             "qwe3" ]
                                                                                                                             SubList [ ListItem
                                                                                                                                       <| MdPara [ Regular
                                                                                                                                                       "asd1" ]
                                                                                                                                       ListItem
                                                                                                                                       <| MdPara [ Regular
                                                                                                                                                       "asd2" ] ]
                                                                                                                             ListItem
                                                                                                                             <| MdPara [ Regular
                                                                                                                                             "zxc1" ]
                                                                                                                             ListItem
                                                                                                                             <| MdPara [ Regular
                                                                                                                                             "sadsafsd" ] ] ] ] ] ] ]

    printfn "%A" <| md
    printfn "%s" <| Md.render md

    0
