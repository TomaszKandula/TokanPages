/**
 * @jest-environment jsdom
 */

import { DelDataFromStorage } from "./delDataFromStorage";
import { IDelDataFromStorage } from "./interface";

describe("Verify helper methods.", () => 
{
    // Prerequisities
    Storage.prototype.removeItem = jest.fn((key: string) => 
    { 
        console.debug(`Called 'localStorage.removeItem' with 'key' argument: ${key}.`);
    });

    it("Given key. When DelDataFromStorage. Should return true.", () => 
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

    it("Given no key. When DelDataFromStorage. Should return false.", () => 
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
