import "../../../../setupTests";
import { IPropsToFields, PropsToFields } from "../../Converters";

describe("Verify PropsToFields.", () => 
{
    it("Given object with properties. When invoke PropsToFields. Should return an array of fields values.", () => 
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
