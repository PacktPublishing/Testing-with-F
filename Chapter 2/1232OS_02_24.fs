namespace chapter02

module _1232OS_02_24 =

    // xml representation
    type Node = 
        | Attribute of string * string
        | Element of Node list
        | Value of string

    // <blockquote cite="Alan Turing">Machines take me by surprise with great frequency</blockquote>
    let quote = 
        Element
            [
                Attribute ("cite", "Alan Turing")
                Value "Machines take me by surprise with great frequency"
            ]
