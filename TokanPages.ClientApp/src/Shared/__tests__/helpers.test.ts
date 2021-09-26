import * as helpers from "../helpers";
import { ITextObject } from "../../Shared/Components/ContentRender/Models/textModel";
import { IErrorDto } from "../../Api/Models";
import { VALIDATION_ERRORS } from "../../Shared/constants";

describe("Verify helper methods.", () => 
{
    // Arrange for all tests
    Storage.prototype.removeItem = jest.fn((key: string) => 
    { 
        console.debug(`Called 'localStorage.removeItem' with 'key' argument: ${key}.`);
    });
    
    Storage.prototype.setItem = jest.fn((key: string, value: any) => 
    {  
        console.debug(`Called 'localStorage.setItem' with 'key' = ${key} and 'value' = ${value}.`);
    });
    
    Storage.prototype.getItem = jest.fn((key: string) => 
    { 
        console.debug(`Called 'localStorage.getItem' with 'key' argument: ${key}.`);
        return "{ \"result\": 0 }";
    });
    
    test("Should convert object props to an array of fields values.", () => 
    {
        // Arrange
        const testObject = 
        {
            FieldA: "ValueA",
            FieldB: "ValueB"
        };
    
        const expectation: string[] = ["ValueA", "ValueB"];
    
        // Act
        // Assert
        expect(
            helpers.ConvertPropsToFields(testObject).sort()
        ).toEqual(
            expectation
        );
    });

    test("Should render HTML line with given HTML tag or return whitespace for undefined input.", () => 
    {
        // Arrange
        const testTag: string = "li";
        const testItem1: string = "This is test item";
        const testItem2 = undefined;
    
        const expectation1: string = "<li>This is test item</li>";
        const expectation2 = " ";
    
        // Act
        // Assert
        expect(helpers.HtmlRenderLine(testTag, testItem1)).toBe(expectation1);
        expect(helpers.HtmlRenderLine(testTag, testItem2)).toBe(expectation2);    
    });

    test("Should render multiple lines of HTML code with given tag.", () => 
    {    
        // Arrange
        const testArray: string[] = ["ValueA", "ValueB"];    
        const testTag: string = "il";
    
        const expectation: string = "<il>ValueA</il><il>ValueB</il>";
    
        // Act
        // Assert
        expect(helpers.HtmlRenderLines(testArray, testTag)).toBe(expectation);
    });

    test("Should return 'n/a' if the same is given.", () =>
    {
        // Arrange
        const sourceDateTime: string = "n/a";
        const expectation: string = "n/a";

        // Act
        // Assert
        expect(helpers.FormatDateTime(sourceDateTime, true)).toBe(expectation);
    });
    
    test("Should return 'n/a' if empty string is given.", () =>
    {
        // Arrange
        const sourceDateTime: string = "";
        const expectation: string = "n/a";

        // Act
        // Assert
        expect(helpers.FormatDateTime(sourceDateTime, true)).toBe(expectation);
    });

    test("Should return 'n/a' if whitespace is given.", () =>
    {
        // Arrange
        const sourceDateTime: string = " ";
        const expectation: string = "n/a";

        // Act
        // Assert
        expect(helpers.FormatDateTime(sourceDateTime, true)).toBe(expectation);
    });

    test("Should return formatted date time: 01/10/2020, 12:15 PM", () =>
    {
        // Arrange
        const sourceDateTime: string = "2020-01-10T12:15:15";
        const expectation: string = "01/10/2020, 12:15 PM";

        // Act
        // Assert
        expect(helpers.FormatDateTime(sourceDateTime, true)).toBe(expectation);
    });

    test("Should return formatted date time: 01/10/2020", () =>
    {
        // Arrange
        const sourceDateTime: string = "2020-01-10T12:15:15";
        const expectation: string = "01/10/2020";

        // Act
        // Assert
        expect(helpers.FormatDateTime(sourceDateTime, false)).toBe(expectation);
    });

    test("Should convert TextObject to raw Text.", () => 
    {
        // Arrange
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

        // Act
        // Assert
        expect(helpers.TextObjectToRawText(textObject)).toBe(expectation);
    });

    test("Should return proper number of words in text.", () => 
    {
        // Arrange
        const rawText: string = "This is test object  We will use it for testing.";
        const expectation: number = 10;

        // Act
        // Assert
        expect(helpers.CountWords(rawText)).toBe(expectation);        
    });

    test("Should return read time for given number of words", () => 
    {
        // Arrange
        const wordsNumber = 700;
        const wordsPerMinute = 130;
        const expectation: string = "5.38";

        // Act
        // Assert
        expect(helpers.GetReadTime(wordsNumber, wordsPerMinute)).toBe(expectation);
    });

    test("Should return translated error message", () => 
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
        const expectation: string = "This user name already exists";

        // Act
        // Assert
        expect(helpers.GetErrorMessage(textObject)).toBe(expectation);
    });

    test("Should return translated error message with validation errors", () => 
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
        const expectation: string = "Cannot add invalid data, " + VALIDATION_ERRORS + ".";

        // Act
        // Assert
        expect(helpers.GetErrorMessage(textObject)).toBe(expectation);
    });

    test("Should delete data from local storage if key is given.", () => 
    {  
        // Act
        // Assert
        expect(helpers.DelDataFromStorage("TestKey1")).toBe(true);
        expect(helpers.DelDataFromStorage("")).toBe(false);
    });

    test("Should write data to local storage for given key and object or array", () => 
    {  
        // Arrange
        const testObject = 
        {  
            result: 0,
            text: "test message"
        };

        const testArray = 
        [
            {
                id: 100,
                flag: 0,
                text: "message 1"
            },
            {
                id: 200,
                flag: 1,
                text: "message 2"
            }
        ];

        const saveObject = helpers.SetDataInStorage(testObject, "TestKey1");
        const saveArray = helpers.SetDataInStorage(testArray, "TestKey2");

        // Act
        // Assert
        expect(saveObject).toBe(true);
        expect(saveArray).toBe(true);
    });

    test("Should read data from local storage for given key", () => 
    {
        // Arrange
        const expectedObject = 
        {
            result: 0
        }

        // Act
        // Assert
        expect(helpers.GetDataFromStorage("TestKey3")).toStrictEqual(expectedObject);
    });
});
