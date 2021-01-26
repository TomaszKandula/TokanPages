import * as helpers from "../helpers";

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
            expectation.sort()
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

        expect(helpers.FormatDateTime(sourceDateTime)).toBe(expectation);
    });
});
