/**
 * @jest-environment jsdom
 */
import { VALIDATION_ERRORS } from "../../../../Shared/constants";
import { IErrorDto } from "../../../../Api/Models";
import { GetErrorMessage } from "./getErrorMessage";
import { IGetErrorMessage } from "./interface";

describe("Verify GetErrorMessage.", () => 
{
    it("Given valid JSON object. When invoke GetErrorMessage. Should return translated error message.", () => 
    {
        // Arrange
        const jsonObject: string = `
        {
            "response": 
            {
                "data":
                {
                    "ErrorCode": "USERNAME_ALREADY_EXISTS",
                    "ErrorMessage": "This user name already exists",
                    "ValidationErrors": null
                }
            }
        }`

        const textObject: IErrorDto = JSON.parse(jsonObject) as IErrorDto;
        const input: IGetErrorMessage = 
        {
            errorObject: textObject
        }
        
        const expectation: string = "This user name already exists";

        // Act
        const result = GetErrorMessage(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("Given valid JSON object with validation errors. When invoke GetErrorMessage. Should return translated error message.", () => 
    {
        // Arrange
        const jsonObject: string = `
        {
            "response": 
            {
                "data":
                {
                    "ErrorCode": "CANNOT_ADD_DATA",
                    "ErrorMessage": "Cannot add invalid data",
                    "ValidationErrors": 
                    [
                        {
                            "PropertyName": "Id",
                            "ErrorCode": "INVALID_GUID",
                            "ErrorMessage": "Must be GUID"
                        },
                        {
                            "PropertyName": "UserAge",
                            "ErrorCode": "INVALID_NUMBER",
                            "ErrorMessage": "Cannot be negative number"
                        }
                    ]
                }
            }            
        }`

        const textObject: IErrorDto = JSON.parse(jsonObject) as IErrorDto;
        const input: IGetErrorMessage = 
        {
            errorObject: textObject
        }

        const expectation: string = "Cannot add invalid data. " + VALIDATION_ERRORS + ".";

        // Act
        const result = GetErrorMessage(input);

        // Assert
        expect(result).toBe(expectation);
    });
});
