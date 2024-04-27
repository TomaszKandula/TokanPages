import { IsRunningOnIOS } from "./Services/IsRunningOnIOS";

export const DELAY_3_SECONDS = 3000;
export const DELAY_30_SECONDS = 30000;
export const SET_INTERVAL_DELAY = IsRunningOnIOS() ? DELAY_3_SECONDS : DELAY_30_SECONDS;

export const LIKES_LIMIT_FOR_ANONYM: number = 25;
export const LIKES_LIMIT_FOR_USER: number = 50;
export const WORDS_PER_MINUTE: number = 265;
export const SELECTED_LANGUAGE: string = "SELECTED_LANGUAGE";
export const RECEIVED_ERROR_MESSAGE: string = "RECEIVED_ERROR_MESSAGE";
export const NO_ERRORS: string = "NO_ERRORS";
export const USER_DATA: string = "USER_DATA";






// --------------------------- TODO: move out below code from constants --------------------------- //
export const PASSWORD_MISSING_CHAR: string = "must contain at least one of the following characters: !, @, #, $, %, ^, &, *";
export const PASSWORD_MISSING_NUMBER: string = "must contain at least one number";
export const PASSWORD_MISSING_LARGE_LETTER: string = "must contain at least one large letter";
export const PASSWORD_MISSING_SMALL_LETTER: string = "must contain at least one small letter";

export const UNEXPECTED_STATUS: string = "Received unexpected status code: {STATUS_CODE}. Please contact IT Support";
export const UNEXPECTED_ERROR: string = "Unexpected error occured";
export const VALIDATION_ERRORS: string = "Validation errors have been found";
export const NULL_RESPONSE_ERROR: string = "The response data has returned null. Possible parsing issue due to invalid JSON/XML format";

export const LIKES_HINT_FOR_ANONYM: string = "You may give up to {LEFT_LIKES} thumbs up!";
export const LIKES_HINT_FOR_USER: string = "You may give up to {LEFT_LIKES} thumbs up!";
export const MAX_LIKES_REACHED: string = "You've reached max thumbs up :)";
