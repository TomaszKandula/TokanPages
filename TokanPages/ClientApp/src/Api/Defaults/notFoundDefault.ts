import { INotFound } from "Api/Models"

export const notFoundDefault: INotFound =
{
    content:
    {
        code: "404",
        header: "Page not found",
        description: "The requested page could not be located. Checkout for any URL misspelling.",
        button: "Return to the homepage"
    }
} 