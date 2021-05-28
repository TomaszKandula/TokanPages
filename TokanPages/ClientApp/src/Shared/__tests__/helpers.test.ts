import * as helpers from "../helpers";
import { ITextObject } from "Shared/Components/ContentRender/Models/textModel";
import { IErrorDto } from "../../Api/Models";
import { VALIDATION_ERRORS } from "../../Shared/constants";

describe("Verify helper methods.", () => 
{
    test("Should convert object props to an array of fields values.", () => 
    {
        const testObject = 
        {
            FieldA: "ValueA",
            FieldB: "ValueB"
        };
    
        const expectation: string[] = ["ValueA", "ValueB"];
    
        expect(
            helpers.ConvertPropsToFields(testObject).sort()
        ).toEqual(
            expectation
        );
    });

    test("Should render HTML line with given HTML tag or return whitespace for undefined input.", () => 
    {
        const testTag: string = "li";
        const testItem1: string = "This is test item";
        const testItem2 = undefined;
    
        const expectation1: string = "<li>This is test item</li>";
        const expectation2 = " ";
    
        expect(helpers.HtmlRenderLine(testTag, testItem1)).toBe(expectation1);
        expect(helpers.HtmlRenderLine(testTag, testItem2)).toBe(expectation2);    
    });

    test("Should render multiple lines of HTML code with given tag.", () => 
    {    
        const testArray: string[] = ["ValueA", "ValueB"];    
        const testTag: string = "il";
    
        const expectation: string = "<il>ValueA</il><il>ValueB</il>";
    
        expect(helpers.HtmlRenderLines(testArray, testTag)).toBe(expectation);
    });

    test("Should return formatted date time: 01/10/2020, 12:15 PM", () =>
    {
        const sourceDateTime: string = "2020-01-10T12:15:15";
        const expectation: string = "01/10/2020, 12:15 PM";

        expect(helpers.FormatDateTime(sourceDateTime, true)).toBe(expectation);
    });

    test("Should return formatted date time: 01/10/2020", () =>
    {
        const sourceDateTime: string = "2020-01-10T12:15:15";
        const expectation: string = "01/10/2020";

        expect(helpers.FormatDateTime(sourceDateTime, false)).toBe(expectation);
    });

    test("Should convert TextObject to raw Text.", () => 
    {
        const jsonObject: string = `
        {
            "items": 
            [
                { 
                    "id": "2d844a70-36d1-4cac-a5a2-89fb01b1c52c",
                    "type": "html",
                    "value": "<h1>This is test object</h1><p>We will use it for testing.</p>",
                    "prop": "",
                    "text": "" 
                }
            ]
        }`

        const textObject: ITextObject = JSON.parse(jsonObject) as ITextObject;
        const expectation: string = "This is test object  We will use it for testing.";

        expect(helpers.TextObjectToRawText(textObject)).toBe(expectation);
    });

    test("Should return proper number of words in text.", () => 
    {
        const rawText: string = "This is test object  We will use it for testing.";
        const expectation: number = 10;
        expect(helpers.CountWords(rawText)).toBe(expectation);        
    });

    test("Should return read time for given number of words", () => 
    {
        const wordsNumber = 700;
        const wordsPerMinute = 130;
        const expectation: string = "5.38";
        expect(helpers.GetReadTime(wordsNumber, wordsPerMinute)).toBe(expectation);
    });

    test("Should return translated error message", () => 
    {
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
        const expectation: string = "This user name already exists";

        expect(helpers.GetErrorMessage(textObject)).toBe(expectation);
    });

    test("Should return translated error message with validation errors", () => 
    {
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
        const expectation: string = "Cannot add invalid data, " + VALIDATION_ERRORS + ".";

        expect(helpers.GetErrorMessage(textObject)).toBe(expectation);
    });
});
