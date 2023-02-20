/**
* @jest-environment jsdom
*/
import "../../../../setupTests";
import { IDelDataFromStorage, DelDataFromStorage } from "..";

describe("verify DelDataFromStorage method", () => 
{
    // Prerequisities
    Storage.prototype.removeItem = jest.fn((key: string) => 
    { 
        console.debug(`Called 'localStorage.removeItem' with 'key' argument: ${key}.`);
    });

    it("should return true, when key is provided.", () => 
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

    it("should return false, when no key is provided.", () => 
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
