import { IRaiseError } from "../States/raiseErrorState";
import { NO_ERRORS } from "../../Shared/constants";

export const RaiseErrorDefault: IRaiseError = 
{
    defaultErrorMessage: NO_ERRORS,
    attachedErrorObject: { }
}
