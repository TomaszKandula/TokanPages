import { ApplicationDialogState } from "../../States";
import { IconType } from "../../../Shared/enums";

export const ApplicationDialog: ApplicationDialogState = {
    title: "",
    message: undefined,
    validation: undefined,
    icon: IconType.info,
    buttons: undefined,
};
