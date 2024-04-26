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


export const NEWSLETTER_SUCCESS: string = "<p>Congratulations!</p><p>You have been added to the newsletter.</p>";
export const NEWSLETTER_WARNING: string = "<span>The newsletter could not be setup, following warning(s) received:</span><ul>{LIST}</ul><span>Please make sure the email is valid and you have access to it.</span>";
export const NEWSLETTER_ERROR: string = "<p>Ouch!</p><p>The newsletter could not be setup.</p><p>{ERROR}.</p>";

export const UPDATE_ARTICLE_SUCCESS: string = "<p>Success!</p><p>Article has been updated successfully</p>";
export const UPDATE_ARTICLE_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>Please check if all required fields are filled correctly.</span>";
export const UPDATE_ARTICLE_ERROR: string = "<p>Ouch!</p><p>The article could not be updated.</p><p>{ERROR}.</p>";
export const SUBSCRIBER_REMOVAL_ERROR: string = "<p>We could not remove your email from our newsletter list.</p><p{ERROR}.</p><p>Please contact us to resolve the issue.</p>";


export const UNEXPECTED_STATUS: string = "Received unexpected status code: {STATUS_CODE}. Please contact IT Support";
export const UNEXPECTED_ERROR: string = "Unexpected error occured";
export const VALIDATION_ERRORS: string = "Validation errors have been found";
export const NULL_RESPONSE_ERROR: string = "The response data has returned null. Possible parsing issue due to invalid JSON/XML format";
export const LIKES_HINT_FOR_ANONYM: string = "You may give up to {LEFT_LIKES} thumbs up!";
export const LIKES_HINT_FOR_USER: string = "You may give up to {LEFT_LIKES} thumbs up!";
export const MAX_LIKES_REACHED: string = "You've reached max thumbs up :)";


export const NEWSLETTER: string = "Newsletter";
export const UPDATE_SUBSCRIBER: string = "Update Subscriber";
export const REMOVE_SUBSCRIBER: string = "Remove Subscriber";
