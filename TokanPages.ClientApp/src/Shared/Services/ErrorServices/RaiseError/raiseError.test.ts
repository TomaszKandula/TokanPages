import "../../../../setupTests";
import { RaiseError } from "..";

describe("Verify RaiseError.", () => {
    it("Given error object with validation error. When invoke RaiseError. Should execute dispatch and return error text.", () => {
        // Arrange
        function dispatch(args: { type: string; errorObject: string }) {
            console.debug(`Dispatch has been called with: ${args}`);
        }

        const input = {
            dispatch: dispatch,
            content: {
                unexpectedStatus: "Status code 400",
                unexpectedError: "Unexpected error occured",
                validationError: "Validation errors have been found",
                nullError: "Null error",
            },
            errorObject: {
                response: {
                    data: {
                        errorCode: "CANNOT_READ_FROM_AZURE_STORAGE",
                        errorMessage: "Cannot read from Azure Storage Blob",
                        validationErrors: [
                            {
                                propertyName: "Id",
                                errorCode: "INVALID_GUID",
                                errorMessage: "Must be GUID",
                            },
                        ],
                    },
                },
            },
        };

        const expectation =
            "Cannot read from Azure Storage Blob. Validation errors have been found: [Id] must be guid;";

        // Act
        const result = RaiseError(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("Given valid JSON object. When invoke RaiseError. Should return translated error message.", () => {
        // Arrange
        function dispatch(args: { type: string; errorObject: string }) {
            console.debug(`Dispatch has been called with: ${args}`);
        }

        const input = {
            dispatch: dispatch,
            content: {
                unexpectedStatus: "Status code 400",
                unexpectedError: "Unexpected error occured",
                validationError: "Validation errors have been found",
                nullError: "Null error",
            },
            errorObject: {
                response: {
                    data: {
                        errorCode: "USERNAME_ALREADY_EXISTS",
                        errorMessage: "This user name already exists",
                        validationErrors: null,
                    },
                },
            },
        };

        const expectation: string = "This user name already exists";

        // Act
        const result = RaiseError(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("Given valid JSON object with validation errors. When invoke RaiseError. Should return translated error message.", () => {
        function dispatch(args: { type: string; errorObject: string }) {
            console.debug(`Dispatch has been called with: ${args}`);
        }

        const input = {
            dispatch: dispatch,
            content: {
                unexpectedStatus: "Status code 400",
                unexpectedError: "Unexpected error occured",
                validationError: "Validation errors have been found",
                nullError: "Null error",
            },
            errorObject: {
                response: {
                    data: {
                        errorCode: "CANNOT_ADD_DATA",
                        errorMessage: "Cannot add invalid data",
                        validationErrors: [
                            {
                                propertyName: "Id",
                                errorCode: "INVALID_GUID",
                                errorMessage: "Must be GUID",
                            },
                            {
                                propertyName: "UserAge",
                                errorCode: "INVALID_NUMBER",
                                errorMessage: "Cannot be negative number",
                            },
                        ],
                    },
                },
            },
        };

        const expectation: string =
            "Cannot add invalid data. " +
            input.content.validationError +
            ": [Id] must be guid; [UserAge] cannot be negative number;";

        // Act
        const result = RaiseError(input);

        // Assert
        expect(result).toBe(expectation);
    });
});
