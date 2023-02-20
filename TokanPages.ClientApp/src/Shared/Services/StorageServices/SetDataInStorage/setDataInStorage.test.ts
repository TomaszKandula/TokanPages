/**
* @jest-environment jsdom
*/
import "../../../../setupTests";
import { SetDataInStorage } from "..";

describe("verify SetDataInStorage method", () => 
{
    // Prerequisities
    Storage.prototype.setItem = jest.fn((key: string, value: any) => 
    {
        console.debug(`Called 'localStorage.setItem' with 'key' = ${key} and 'value' = ${value}.`);
    });

    it("should return true, when input Object provided.", () => 
    {  
        // Arrange
        const testObject = 
        {  
            result: 0,
            text: "test message"
        };

        const input = 
        {
            selection: testObject,
            key: "SomeKey"
        }

        // Act
        const saveObject = SetDataInStorage(input);

        // Assert
        expect(saveObject).toBe(true);
    });

    it("should return false, when input Object and empty Key provided.", () => 
    {  
        // Arrange
        const testObject = 
        {  
            result: 0,
            text: "test message"
        };

        const input = 
        {
            selection: testObject,
            key: ""
        }

        // Act
        const saveObject = SetDataInStorage(input);

        // Assert
        expect(saveObject).toBe(false);
    });

    it("should return true, when input Array provided.", () => 
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

        const input = 
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