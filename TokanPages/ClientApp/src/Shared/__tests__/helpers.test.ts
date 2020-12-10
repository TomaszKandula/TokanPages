import * as helpers from "../helpers";

describe("Verify helper methods.", () => 
{

    test("Should convert object props to an array of fields values.", () => 
    {
    
        const TestObject = 
        {
            FieldA: "ValueA",
            FieldB: "ValueB"
        };
    
        const Expectation: string[] = ["ValueA", "ValueB"];
    
        expect(
            helpers.ConvertPropsToFields(TestObject).sort()
        ).toEqual(
            Expectation.sort()
        );
    
    });

    test("Should render HTML line with given HTML tag or return whitespace for undefined input.", () => 
    {
    
        const TestTag: string = "li";
        const TestItem1: string = "This is test item";
        const TestItem2 = undefined;
    
        const Expectation1: string = "<li>This is test item</li>";
        const Expectation2 = " ";
    
        expect(helpers.HtmlRenderLine(TestTag, TestItem1)).toBe(Expectation1);
        expect(helpers.HtmlRenderLine(TestTag, TestItem2)).toBe(Expectation2);
    
    });

    test("Should render multiple lines of HTML code with given tag.", () => 
    {
    
        const TestArray: string[] = ["ValueA", "ValueB"];    
        const TestTag: string = "il";
    
        const Expectation: string = "<il>ValueA</il><il>ValueB</il>";
    
        expect(helpers.HtmlRenderLines(TestArray, TestTag)).toBe(Expectation);
    
    });

});
