import "../../../../setupTests";
import { IGetShortText, GetShortText } from "../../Utilities";

describe("Verify GetShortText.", () => 
{
    it("Given long text and limit of 10 words. When invoke GetShortText. Should return text with 10 words.", () => 
    {
        // Arrange
        const expectation = "This is long text. It will be used to test...";
        const input: IGetShortText = 
        {
            value: "This is long text. It will be used to test the method that should make it shorter.",
            limit: 10
        }

        // Act
        const output = GetShortText(input);

        // Assert
        expect(output).toBe(expectation);
     });
 
    it("Given empty input value. When invoke GetShortText. Should return empty string.", () => 
    {
        // Arrange
        const expectation = "";
        const input: IGetShortText = 
        {
            value: "",
            limit: 10
        }

        // Act
        const output = GetShortText(input);

        // Assert
        expect(output).toBe(expectation);
    });
});
 