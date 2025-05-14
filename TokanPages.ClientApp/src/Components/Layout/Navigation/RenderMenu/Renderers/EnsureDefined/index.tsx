import * as React from "react";
import Validate from "validate.js";

interface Properties {
    values: any[];
    messages: string[];
}

export const EnsureDefined = (object: Properties, onSuccess: React.ReactElement): React.ReactElement => {
    if (object.values.length !== object.messages.length) return <div></div>;

    for (let index = 0; index < object.values.length; index++) {
        if (!Validate.isDefined(object.values[index])) {
            return <div>${object.messages[index]}</div>;
        }
    }

    return onSuccess;
};

interface EnsureDefinedExtProps {
    object: React.ReactElement;
    hasErrors: boolean;
    hasWarnings: boolean;
}

export const EnsureDefinedExt = (object: Properties, onSuccess: React.ReactElement): EnsureDefinedExtProps => {
    if (object.values.length !== object.messages.length)
        return {
            object: <div></div>,
            hasErrors: true,
            hasWarnings: false,
        };

    for (let index = 0; index < object.values.length; index++) {
        if (!Validate.isDefined(object.values[index])) {
            return {
                object: <div>${object.messages[index]}</div>,
                hasErrors: false,
                hasWarnings: true,
            };
        }
    }

    return {
        object: onSuccess,
        hasErrors: false,
        hasWarnings: false,
    };
};
