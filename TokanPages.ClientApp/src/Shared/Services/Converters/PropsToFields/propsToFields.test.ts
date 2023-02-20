import "../../../../setupTests";
import { IPropsToFields, PropsToFields } from "..";

describe("verify PropsToFields method", () => 
{
    it("should return an array of fields values, when object with properties provided.", () => 
    {
        // Arrange
        const expectation: string[] = ["ValueA", "ValueB"];
        const input: IPropsToFields  = 
        {
            object: 
            {
                FieldA: "ValueA",
                FieldB: "ValueB"
            }
        }
     
        // Act
        const result = PropsToFields(input).sort();

        // Assert
        expect(result).toEqual(expectation);
    });
});
