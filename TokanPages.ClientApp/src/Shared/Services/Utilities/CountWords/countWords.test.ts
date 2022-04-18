/**
 * @jest-environment jsdom
 */

import { CountWords } from "./countWords";
import { ICountWords } from "./interface";

describe("Verify helper methods.", () =>
{
    it("Given text. When CountWords. Should return proper number of words in text.", () => 
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
});
