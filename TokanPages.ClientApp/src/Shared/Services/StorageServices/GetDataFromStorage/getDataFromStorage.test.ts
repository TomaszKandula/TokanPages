/**
* @jest-environment jsdom
*/
import "../../../../setupTests";
import { IGetDataFromStorage, GetDataFromStorage } from "..";

describe("Verify GetDataFromStorage.", () => 
{ 
    it("Given item under known key. When invoke GetDataFromStorage. Should read data from local storage.", () => 
    {
        // Arrange
        Storage.prototype.getItem = jest.fn((key: string) => 
        { 
            console.debug(`Called 'localStorage.getItem' with 'key' argument: ${key}.`);
            if (key !== "SomeKey") return "";
            return "{ \"result\": 0 }";
        });

        const expectedObject = { result: 0 }
        const input: IGetDataFromStorage = 
        {
            key: "SomeKey"
        }

        // Act
        const result = GetDataFromStorage(input);

        // Assert
        expect(result).toStrictEqual(expectedObject);
    });

    it("Given item under unknown key. When invoke GetDataFromStorage. Should return empty object.", () => 
    {
        // Arrange
        Storage.prototype.getItem = jest.fn((key: string) => 
        { 
            console.debug(`Called 'localStorage.getItem' with 'key' argument: ${key}.`);
            if (key !== "SomeKey") return "";
            return "{ \"result\": 100 }";
        });

        const expectedObject = { }
        const input: IGetDataFromStorage = 
        {
            key: "AnotherKey"
        }

        // Act
        const result = GetDataFromStorage(input);

        // Assert
        expect(result).toStrictEqual(expectedObject);
    });

    it("Given invalid item under known key. When invoke GetDataFromStorage. Should return empty object.", () => 
    {
        // Arrange
        Storage.prototype.getItem = jest.fn((key: string) => 
        { 
            console.debug(`Called 'localStorage.getItem' with 'key' argument: ${key}.`);
            if (key !== "SomeKey") return "";
            return "{ result: 'should fail' }";
        });

        const expectedObject = { }
        const input: IGetDataFromStorage = 
        {
            key: "SomeKey"
        }

        // Act
        const result = GetDataFromStorage(input);

        // Assert
        expect(result).toStrictEqual(expectedObject);
    });
});
