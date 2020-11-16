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
                minimum: 3,
                message: "must be at least 3 characters"
            }
        },
        LastName:
        {
            presence: true,
            length: 
            {
                minimum: 3,
                message: "must be at least 3 characters"
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
                minimum: 25,
                message: "must be at least 25 characters"
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
