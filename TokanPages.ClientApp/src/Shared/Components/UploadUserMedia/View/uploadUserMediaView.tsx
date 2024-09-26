import * as React from "react";
import BackupIcon from "@material-ui/icons/Backup";
import { IconButton } from "@material-ui/core";
import { UploadUserMediaStyle } from "./uploadUserMediaStyle";
import { ReactChangeEvent } from "../../../../Shared/types";
import { v4 as uuidv4 } from "uuid";

interface Properties {
    customHandle?: string;
    buttonState: boolean;
    inputHandler: (event: ReactChangeEvent) => void;
    accepting: string;
}

export const UploadUserMediaView = (props: Properties): React.ReactElement => {
    const classes = UploadUserMediaStyle();
    const uuid = uuidv4();
    return (
        <label htmlFor={props.customHandle ? props.customHandle : `userFile-${uuid}`}>
            <input
                hidden
                accept={props.accepting}
                id={props.customHandle ? props.customHandle : `userFile-${uuid}`}
                name={props.customHandle ? props.customHandle : `userFile-${uuid}`}
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
