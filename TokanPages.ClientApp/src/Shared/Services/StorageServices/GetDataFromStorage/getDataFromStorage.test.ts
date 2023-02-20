/**
* @jest-environment jsdom
*/
import "../../../../setupTests";
import { GetDataFromStorage } from "..";

describe("verify GetDataFromStorage method", () => 
{ 
    it("should read data from local storage, when item under known key is provided.", () => 
    {
        // Arrange
        Storage.prototype.getItem = jest.fn((key: string) => 
        { 
            console.debug(`Called 'localStorage.getItem' with 'key' argument: ${key}.`);
            if (key !== "SomeKey") return "";
            return "{ \"result\": 0 }";
        });

        const expectedObject = { result: 0 }
        const input = 
        {
            key: "SomeKey"
        }

        // Act
        const result = GetDataFromStorage(input);

        // Assert
        expect(result).toStrictEqual(expectedObject);
    });

    it("should return empty object, when item under unknown key is provided.", () => 
    {
        // Arrange
        Storage.prototype.getItem = jest.fn((key: string) => 
        { 
            console.debug(`Called 'localStorage.getItem' with 'key' argument: ${key}.`);
            if (key !== "SomeKey") return "";
            return "{ \"result\": 100 }";
        });

        const expectedObject = { }
        const input = 
        {
            key: "AnotherKey"
        }

        // Act
        const result = GetDataFromStorage(input);

        // Assert
        expect(result).toStrictEqual(expectedObject);
    });

    it("should return empty object, when invalid item under known key is provided.", () => 
    {
        // Arrange
        Storage.prototype.getItem = jest.fn((key: string) => 
        { 
            console.debug(`Called 'localStorage.getItem' with 'key' argument: ${key}.`);
            if (key !== "SomeKey") return "";
            return "{ result: 'should fail' }";
        });

        const expectedObject = { }
        const input = 
        {
            key: "SomeKey"
        }

        // Act
        const result = GetDataFromStorage(input);

        // Assert
        expect(result).toStrictEqual(expectedObject);
    });
});
