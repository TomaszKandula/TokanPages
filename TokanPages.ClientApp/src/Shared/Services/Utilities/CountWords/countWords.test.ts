import "../../../../setupTests";
import { ICountWords, CountWords } from "..";

describe("Verify CountWords.", () =>
{
    it("Given text. When invoke CountWords. Should return proper number of words in text.", () => 
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

    it("Given undefined input. When invoke CountWords. Should return zero.", () => 
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
