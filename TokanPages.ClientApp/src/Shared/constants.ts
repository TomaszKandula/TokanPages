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

export const MESSAGE_OUT_SUCCESS: string = "<p>Congratulations!</p><p>The message has been sent successfully, we will try to respond as soon as possible.</p>";
export const MESSAGE_OUT_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To send an email all fields must be filled along with acceptance of Terms of Use and Privacy Policy.</span>";
export const MESSAGE_OUT_ERROR: string = "<p>Ouch!</p><p>The message could not be sent.</p><p>{ERROR}.</p>";

export const SIGNIN_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To sign-in all fields must be filled properly.</span>";
export const SIGNUP_SUCCESS: string = "<p>Congratulations!</p><p>The account has been setup successfully, please check your e-mail and follow given instructions to make newly created account active.</p>";
export const SIGNUP_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To sign-up all fields must be filled along with acceptance of Terms of Use and Privacy Policy.</span>";

export const RESET_PASSWORD_SUCCESS: string = "<p>The password has been reset. Please check your email box and follow the instruction to setup new password.</p>";
export const RESET_PASSWORD_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To reset user password registered and verified e-mail address must be provided.</span>";

export const DEACTIVATE_USER: string = "<p>Your data has been preserved, and your account has been deactivated.</p><p>If you would like to activate it again, please send a request to IT Support.</p>";

export const REMOVE_USER: string = "<p>Your account has been permanently deleted from the system along with related data.</p>";
export const UPDATE_USER_SUCCESS: string = "<p>Congratulations!</p><p>Your data has been updated.</p>";
export const UPDATE_USER_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To update your data you must provide values that complies to the requirements.</span>";
export const UPDATE_PASSWORD_SUCCESS: string = "<p>Congratulations!</p><p>Your password is now set. You may log in again.</p>";
export const UPDATE_PASSWORD_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To update your password you must provide values that complies to the requirements.</span>";

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




export const SIGNIN_FORM: string = "Signin Form";
export const SIGNUP_FORM: string = "Signup Form";
export const RESET_FORM: string = "Password Reset";
export const UPDATE_FORM: string = "Update Password";
export const CONTACT_FORM: string = "Contact Form";
export const ACCOUNT_FORM: string = "Account Settings";
export const NEWSLETTER: string = "Newsletter";
export const UPDATE_SUBSCRIBER: string = "Update Subscriber";
export const REMOVE_SUBSCRIBER: string = "Remove Subscriber";
