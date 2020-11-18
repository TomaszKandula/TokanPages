import Validate from "validate.js";

interface IValidateInputs
{
    FirstName: string;
    LastName:  string;
    Email:     string;
    Subject:   string;
    Message:   string;
    Terms:     boolean;
}

function ValidateInputs(props: IValidateInputs): any
{

    let Constraints = 
    {
        FirstName:
        {
            presence: true,
            length: 
            {
                minimum: 2,
                message: "must be at least 2 characters"
            }
        },
        LastName:
        {
            presence: true,
            length: 
            {
                minimum: 2,
                message: "must be at least 2 characters"
            }
        },
        Email: 
        {
            email: 
            {
                message: "does not look like a valid email"
            }
        },
        Subject:
        {
            presence: true,
            length: 
            {
                minimum: 6,
                message: "must be at least 6 characters"
            }
        },
        Message:
        {
            presence: true,
            length: 
            {
                minimum: 20,
                message: "must be at least 20 characters"
            }
        },
        Terms:
        {
            presence: true,
            inclusion:
            {
                within: [true],
                message: "^You must accept terms of use and privacy policy"
            }
        }
    };

    let Results = Validate(
    {
        FirstName: props.FirstName,
        LastName:  props.LastName,
        Email:     props.Email,
        Subject:   props.Subject,
        Message:   props.Message,
        Terms:     props.Terms
    }, 
    Constraints);

    return Results;

}

export 
{
    ValidateInputs
}
