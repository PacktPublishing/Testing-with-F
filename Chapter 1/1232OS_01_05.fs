module _1232OS_01_05
    let div x y =
        // precondition
        assert (y > 0)
        assert (x > y)

        let result = x / y

        // postcondition
        assert (result > 0)
        result