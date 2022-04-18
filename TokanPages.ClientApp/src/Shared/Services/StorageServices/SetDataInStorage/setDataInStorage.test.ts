/**
 * @jest-environment jsdom
 */
import { ISetDataInStorage } from "./interface";
import { SetDataInStorage } from "./setDataInStorage";

describe("Verify helper methods.", () => 
{
    // Prerequisities
    Storage.prototype.setItem = jest.fn((key: string, value: any) => 
    {
        console.debug(`Called 'localStorage.setItem' with 'key' = ${key} and 'value' = ${value}.`);
    });

    it("Given input Object. When SetDataInStorage. Should return true.", () => 
    {  
        // Arrange
        const testObject = 
        {  
            result: 0,
            text: "test message"
        };

        const input: ISetDataInStorage = 
        {
            selection: testObject,
            key: "SomeKey"
        }

        // Act
        const saveObject = SetDataInStorage(input);

        // Assert
        expect(saveObject).toBe(true);
    });

    it("Given input Object and empty Key. When SetDataInStorage. Should return false.", () => 
    {  
        // Arrange
        const testObject = 
        {  
            result: 0,
            text: "test message"
        };

        const input: ISetDataInStorage = 
        {
            selection: testObject,
            key: ""
        }

        // Act
        const saveObject = SetDataInStorage(input);

        // Assert
        expect(saveObject).toBe(false);
    });

    it("Given input Array. When SetDataInStorage. Should return true.", () => 
    {  
        // Arrange
        const testArray = 
        [
            {
                id: 100,
                flag: 0,
                text: "message 1"
            },
            {
                id: 200,
                flag: 1,
                text: "message 2"
            }
        ];

        const input: ISetDataInStorage = 
        {
            selection: testArray,
            key: "SomeKey"
        }

        // Act
        const saveArray = SetDataInStorage(input);

        // Assert
        expect(saveArray).toBe(true);
    });
});