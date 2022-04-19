import "../../../../setupTests";
import { IGetReadTime, GetReadTime } from "../../Utilities";

describe("Verify GetReadTime.", () => 
{
    it("Given number of words and words per minute. When invoke GetReadTime. Should return read time.", () => 
    {
        // Arrange
        const expectation: string = "5.38";
         const input: IGetReadTime = 
        {
            countWords: 700,
            wordsPerMinute: 130
        }

        // Act
        const result = GetReadTime(input);

        // Assert
        expect(result).toBe(expectation);
    });
});
