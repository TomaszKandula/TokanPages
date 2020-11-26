import Validate from "validate.js";

function ValidateEmail(email: string): any
{

    let Constraints = 
    {
        Email: 
        {
            email: 
            {
                message: "does not look like a valid email"
            }
        }
    };

    let Results = Validate({ Email: email }, Constraints);
    return Results;

}

export 
{
    ValidateEmail
}
