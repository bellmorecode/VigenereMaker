# Vigenere Maker 

## About this project

The Vigenere app has a lot of markup because it contains a grid of letters that is 26 x 26.  That's 676 elements and they are all bound to the same object in the ViewModel. 

This is a Console App that was used to generate the markup for the main app. 

This is the pattern that is repeated: 

> var pattern0 = "<TextBlock Grid.Row=\"{ROW}\" Grid.Column=\"{COLUMN}\" Style=\"{StaticResource SquareCell}\">" + 
>                "<TextBlock.Resources>" +
>                    "<sys:Int32 x:Key=\"CurrentRow\">{ROW}</sys:Int32>" +
>                    "<sys:Int32 x:Key=\"CurrentCol\">{COLUMN}</sys:Int32>" + 
>                "</TextBlock.Resources>" + 
>                "<TextBlock.Text>" + 
>                    "<MultiBinding Converter=\"{StaticResource MatrixCell}\">" +
>                        "<Binding Path=\"VigenereMatrix\"></Binding>" +
>                        "<Binding Source=\"{StaticResource CurrentRow}\" ></Binding>" +
>                        "<Binding Source=\"{StaticResource CurrentCol}\" ></Binding>" +
>                   "</MultiBinding>"
>                + "</TextBlock.Text>"
>                + "<TextBlock.Foreground>"
>                    + "<MultiBinding Converter=\"{StaticResource mcfg}\">"
>                        +"<Binding Source=\"{StaticResource CurrentRow}\" ></Binding>"
>                        +"<Binding Source=\"{StaticResource CurrentCol}\" ></Binding>"
>                        + "<Binding Path=\"MatrixSelectedRow\"></Binding>"
>                        + "<Binding Path=\"MatrixSelectedColumn\"></Binding>"
>                    + "</MultiBinding>"
>                + "</TextBlock.Foreground>"
>            + "</TextBlock>";
