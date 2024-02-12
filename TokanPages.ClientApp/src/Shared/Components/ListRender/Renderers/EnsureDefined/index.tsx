import * as React from "react";
import Validate from "validate.js";

interface Properties {
    values: any[];
    messages: string[];
}

export const EnsureDefined = (object: Properties, onSuccess: JSX.Element): JSX.Element => {
    if (object.values.length !== object.messages.length) return <div></div>;

    for (let index = 0; index < object.values.length; index++) {
        if (!Validate.isDefined(object.values[index])) {
            return <div>${object.messages[index]}</div>;
        }
    }

    return onSuccess;
};
