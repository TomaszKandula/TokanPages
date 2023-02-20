import "../../../../setupTests";
import { IGetShortText, GetShortText } from "..";

describe("verify GetShortText method", () => 
{
    it("should return text with 10 words, when long text and limit of 10 words provided.", () => 
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
 
    it("should return empty string, when empty input value provided.", () => 
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
 