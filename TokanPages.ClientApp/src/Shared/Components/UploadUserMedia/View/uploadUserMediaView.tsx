import * as React from "react";
import BackupIcon from "@material-ui/icons/Backup";
import { IconButton } from "@material-ui/core";
import { UploadUserMediaStyle } from "./uploadUserMediaStyle";
import { v4 as uuidv4 } from "uuid";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    isUploading: boolean;
    buttonState: boolean;
    inputHandler: any;
    accepting: string;
}

export const UploadUserMediaView = (props: IBinding): JSX.Element => 
{
    const classes = UploadUserMediaStyle();
    const uuid = uuidv4();
    return(
        <label htmlFor={`userFile-${uuid}`}>
            <input
                hidden
                accept={props.bind?.accepting}
                id={`userFile-${uuid}`}
                name={`userFile-${uuid}`}
                type="file"
                multiple={false}
                onChange={props.bind?.inputHandler}
            />
            <IconButton 
                component="span"
                size="small"
                disabled={!props.bind?.buttonState} 
                className={classes.button_upload}
            >
                <BackupIcon />
            </IconButton>
        </label>
    );
}