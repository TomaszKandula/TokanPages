import * as React from "react";
import BackupIcon from "@material-ui/icons/Backup";
import { IconButton } from "@material-ui/core";
import { UploadUserMediaStyle } from "./uploadUserMediaStyle";
import { ReactChangeEvent } from "../../../../Shared/types";
import { v4 as uuidv4 } from "uuid";

interface Properties {
    buttonState: boolean;
    inputHandler: (event: ReactChangeEvent) => void;
    accepting: string;
}

export const UploadUserMediaView = (props: Properties): JSX.Element => {
    const classes = UploadUserMediaStyle();
    const uuid = uuidv4();
    return (
        <label htmlFor={`userFile-${uuid}`}>
            <input
                hidden
                accept={props.accepting}
                id={`userFile-${uuid}`}
                name={`userFile-${uuid}`}
                type="file"
                multiple={false}
                onChange={props.inputHandler}
            />
            <IconButton component="span" size="small" disabled={!props.buttonState} className={classes.button_upload}>
                <BackupIcon />
            </IconButton>
        </label>
    );
};
