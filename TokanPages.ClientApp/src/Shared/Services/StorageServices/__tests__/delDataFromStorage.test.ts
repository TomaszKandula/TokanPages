/**
* @jest-environment jsdom
*/
import "../../../../setupTests";
import { IDelDataFromStorage, DelDataFromStorage } from "../../StorageServices";

describe("Verify DelDataFromStorage.", () => 
{
    // Prerequisities
    Storage.prototype.removeItem = jest.fn((key: string) => 
    { 
        console.debug(`Called 'localStorage.removeItem' with 'key' argument: ${key}.`);
    });

    it("Given key. When invoke DelDataFromStorage. Should return true.", () => 
    {  
        // Arrange
        const input: IDelDataFromStorage = 
        {
            key: "SomeKey"
        }

        // Act
        const result = DelDataFromStorage(input);

        // Assert
        expect(result).toBe(true);
    });

    it("Given no key. When invoke DelDataFromStorage. Should return false.", () => 
    {  
        // Arrange
        const input: IDelDataFromStorage = 
        {
            key: ""
        }

        // Act
        const result = DelDataFromStorage(input);

        // Assert
        expect(result).toBe(false);
    });
});
