/**
 * @jest-environment jsdom
 */

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
        
    it("Given object with properties. When ConvertPropsToFields. Should return an array of fields values.", () => 
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

    it("Given HTML tag. When HtmlRenderLine. Should render HTML line.", () => 
    {
        // Arrange
        const testTag: string = "li";
        const testItem1: string = "This is test item";
    
        const expectation1: string = "<li>This is test item</li>";
    
        // Act
        // Assert
        expect(helpers.HtmlRenderLine(testTag, testItem1)).toBe(expectation1);
    });

    it("Given HTML tag and undefined text. When HtmlRenderLine. Should return whitespace.", () => 
    {
        // Arrange
        const testTag: string = "li";
        const testItem2 = undefined;
        const expectation2 = " ";
    
        // Act
        // Assert
        expect(helpers.HtmlRenderLine(testTag, testItem2)).toBe(expectation2);
    });

    it("Given HTML tag and array of strings. When HtmlRenderLines. Should return multiple lines of HTML code.", () => 
    {    
        // Arrange
        const testArray: string[] = ["ValueA", "ValueB"];    
        const testTag: string = "il";
    
        const expectation: string = "<il>ValueA</il><il>ValueB</il>";
    
        // Act
        // Assert
        expect(helpers.HtmlRenderLines(testArray, testTag)).toBe(expectation);
    });

    it("Given 'n/a' value. When FormatDateTime. Should return 'n/a'.", () =>
    {
        // Arrange
        const sourceDateTime: string = "n/a";
        const expectation: string = "n/a";

        // Act
        // Assert
        expect(helpers.FormatDateTime(sourceDateTime, true)).toBe(expectation);
    });
    
    it("Given empty string. When FormatDateTime. Should return 'n/a'.", () =>
    {
        // Arrange
        const sourceDateTime: string = "";
        const expectation: string = "n/a";

        // Act
        // Assert
        expect(helpers.FormatDateTime(sourceDateTime, true)).toBe(expectation);
    });

    it("Given whitespace. When FormatDateTime. Should return 'n/a'.", () =>
    {
        // Arrange
        const sourceDateTime: string = " ";
        const expectation: string = "n/a";

        // Act
        // Assert
        expect(helpers.FormatDateTime(sourceDateTime, true)).toBe(expectation);
    });

    it("Given unformatted date and time. When FormatDateTime. Should return formatted date time.", () =>
    {
        // Arrange
        const sourceDateTime: string = "2020-01-10T12:15:15";
        const expectation: string = "01/10/2020, 12:15 PM";

        // Act
        // Assert
        expect(helpers.FormatDateTime(sourceDateTime, true)).toBe(expectation);
    });

    it("Given unformatted date and time. When FormatDateTime wihtout time. Should return formatted date only.", () =>
    {
        // Arrange
        const sourceDateTime: string = "2020-01-10T12:15:15";
        const expectation: string = "01/10/2020";

        // Act
        // Assert
        expect(helpers.FormatDateTime(sourceDateTime, false)).toBe(expectation);
    });

    it("Given valid JSON object with HTML type. When TextObjectToRawText. Should return string.", () => 
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

    it("Given valid JSON object without HTML type. When TextObjectToRawText. Should return empty string.", () => 
    {
        // Arrange
        const jsonObject: string = `
        {
            "items": 
            [
                { 
                    "id": "2d844a70-36d1-4cac-a5a2-89fb01b1c52c",
                    "type": "image",
                    "value": "<img src='http://localhost:5000/wallpaper.jpg' alt='' />",
                    "prop": "",
                    "text": "" 
                }
            ]
        }`

        const textObject: ITextObject = JSON.parse(jsonObject) as ITextObject;

        // Act
        // Assert
        expect(helpers.TextObjectToRawText(textObject)).toBe("");
    });

    it("Given undefined input. When TextObjectToRawText. Should return undefined.", () => 
    {
        // Arrange
        // Act
        // Assert
        expect(helpers.TextObjectToRawText(undefined)).toBe(undefined);
    });

    it("Given empty JSON object. When TextObjectToRawText. Should return undefined.", () => 
    {
        // Arrange
        const jsonObject: string = `
        {
            "items": [ ]
        }`

        const textObject: ITextObject = JSON.parse(jsonObject) as ITextObject;

        // Act
        // Assert
        expect(helpers.TextObjectToRawText(textObject)).toBe(undefined);
    });

    it("Given text. When CountWords. Should return proper number of words in text.", () => 
    {
        // Arrange
        const rawText: string = "This is test object  We will use it for testing.";
        const expectation: number = 10;

        // Act
        // Assert
        expect(helpers.CountWords(rawText)).toBe(expectation);        
    });

    it("Given number of words and words per minute. When GetReadTime. Should return read time.", () => 
    {
        // Arrange
        const wordsNumber = 700;
        const wordsPerMinute = 130;
        const expectation: string = "5.38";

        // Act
        // Assert
        expect(helpers.GetReadTime(wordsNumber, wordsPerMinute)).toBe(expectation);
    });

    it("Given long text and limit of 10 words. When GetShortText. Should return text with 10 words.", () => 
    {
        // Arrange
        const text = "This is long text. It will be used to test the method that should make it shorter.";
        const expectation = "This is long text. It will be used to test...";
        const limit = 10;

        // Act
        const output = helpers.GetShortText(text, limit);

        // Assert
        expect(output).toBe(expectation);
    });

    it("Given empty input value. When GetShortText. Should return empty string.", () => 
    {
        // Arrange
        const text = "";
        const expectation = "";
        const limit = 10;

        // Act
        const output = helpers.GetShortText(text, limit);

        // Assert
        expect(output).toBe(expectation);
    });

    it("Given valid JSON object. When GetErrorMessage. Should return translated error message.", () => 
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

    it("Given valid JSON object with validation errors. When GetErrorMessage. Should return translated error message.", () => 
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

    it("Given key. When DelDataFromStorage. Should return true.", () => 
    {  
        // Act
        // Assert
        expect(helpers.DelDataFromStorage("SomeKey")).toBe(true);
    });

    it("Given no key. When DelDataFromStorage. Should return false.", () => 
    {  
        // Act
        // Assert
        expect(helpers.DelDataFromStorage("")).toBe(false);
    });

    it("Given input Object. When SetDataInStorage. Should return true.", () => 
    {  
        // Arrange
        const testObject = 
        {  
            result: 0,
            text: "test message"
        };

        const saveObject = helpers.SetDataInStorage(testObject, "SomeKey");

        // Act
        // Assert
        expect(saveObject).toBe(true);
    });

    it("Given input Object and empty Key. When SetDataInStorage. Should return false.", () => 
    {  
        // Arrange
        const testObject = 
        {  
            result: 0,
            text: "test message"
        };

        const saveObject = helpers.SetDataInStorage(testObject, "");

        // Act
        // Assert
        expect(saveObject).toBe(false);
    });

    it("Given input Array. When SetDataInStorage. Should return true.", () => 
    {  
        // Arrange
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

        const saveArray = helpers.SetDataInStorage(testArray, "SomeKey");

        // Act
        // Assert
        expect(saveArray).toBe(true);
    });

    it("Given item under known key. When GetDataFromStorage. Should read data from local storage.", () => 
    {
        // Arrange
        Storage.prototype.getItem = jest.fn((key: string) => 
        { 
            console.debug(`Called 'localStorage.getItem' with 'key' argument: ${key}.`);
            if (key !== "SomeKey") return "";
            return "{ \"result\": 0 }";
        });

        const expectedObject = { result: 0 }

        // Act
        // Assert
        expect(helpers.GetDataFromStorage("SomeKey")).toStrictEqual(expectedObject);
    });

    it("Given item under unknown key. When GetDataFromStorage. Should return empty object.", () => 
    {
        // Arrange
        Storage.prototype.getItem = jest.fn((key: string) => 
        { 
            console.debug(`Called 'localStorage.getItem' with 'key' argument: ${key}.`);
            if (key !== "SomeKey") return "";
            return "{ \"result\": 100 }";
        });

        const expectedObject = { }

        // Act
        // Assert
        expect(helpers.GetDataFromStorage("AnotherKey")).toStrictEqual(expectedObject);
    });

    it("Given invalid item under known key. When GetDataFromStorage. Should return empty object.", () => 
    {
        // Arrange
        Storage.prototype.getItem = jest.fn((key: string) => 
        { 
            console.debug(`Called 'localStorage.getItem' with 'key' argument: ${key}.`);
            if (key !== "SomeKey") return "";
            return "{ result: 'should fail' }";
        });

        const expectedObject = { }

        // Act
        // Assert
        expect(helpers.GetDataFromStorage("SomeKey")).toStrictEqual(expectedObject);
    });
});
