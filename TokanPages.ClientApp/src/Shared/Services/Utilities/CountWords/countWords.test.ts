import "../../../../setupTests";
import { ICountWords, CountWords } from "..";

describe("verify CountWords method", () =>
{
    it("should return proper number of words in text, when text is provided.", () => 
    {
        // Arrange
        const expectation: number = 10;
        const input: ICountWords = 
        {
            inputText: "This is test object  We will use it for testing."
        }

        // Act
        const result = CountWords(input);

        // Assert
        expect(result).toBe(expectation);        
    });

    it("should return zero, when undefined input is provided.", () => 
    {
        // Arrange
        const expectation: number = 0;
        const input: ICountWords = 
        {
            inputText: undefined
        }

        // Act
        const result = CountWords(input);

        // Assert
        expect(result).toBe(expectation);        
    });
});
