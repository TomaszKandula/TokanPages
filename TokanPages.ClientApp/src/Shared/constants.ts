const API_VER = process.env.REACT_APP_API_VER;

/* BASE URL */

export const APP_FRONTEND = process.env.REACT_APP_FRONTEND;
export const APP_BACKEND = process.env.REACT_APP_BACKEND;

/* API | ARTICLES */

export const API_QUERY_GET_ARTICLES = `${APP_BACKEND}/api/v${API_VER}/articles/GetAllArticles/`;
export const API_QUERY_GET_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/GetArticle/{id}/`;
export const API_COMMAND_ADD_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/AddArticle/`;
export const API_COMMAND_UPDATE_ARTICLE_CONTENT = `${APP_BACKEND}/api/v${API_VER}/articles/UpdateArticleContent/`;
export const API_COMMAND_UPDATE_ARTICLE_COUNT = `${APP_BACKEND}/api/v${API_VER}/articles/UpdateArticleCount/`;
export const API_COMMAND_UPDATE_ARTICLE_LIKES = `${APP_BACKEND}/api/v${API_VER}/articles/UpdateArticleLikes/`;
export const API_COMMAND_UPDATE_ARTICLE_VISIBILITY = `${APP_BACKEND}/api/v${API_VER}/articles/UpdateArticleVisibility/`;
export const API_COMMAND_REMOVE_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/RemoveArticle/`;

/* API | USERS */

export const API_QUERY_GET_USERS = `${APP_BACKEND}/api/v${API_VER}/users/GetAllUsers/`;
export const API_QUERY_GET_USER = `${APP_BACKEND}/api/v${API_VER}/users/GetUser/{id}/`;
export const API_COMMAND_ADD_USER = `${APP_BACKEND}/api/v${API_VER}/users/AddUser/`;
export const API_COMMAND_ACTIVATE_USER = `${APP_BACKEND}/api/v${API_VER}/users/ActivateUser/`;
export const API_COMMAND_UPDATE_USER = `${APP_BACKEND}/api/v${API_VER}/users/UpdateUser/`;
export const API_COMMAND_REMOVE_USER = `${APP_BACKEND}/api/v${API_VER}/users/RemoveUser/`;
export const API_COMMAND_AUTHENTICATE = `${APP_BACKEND}/api/v${API_VER}/users/AuthenticateUser/`;
export const API_COMMAND_REAUTHENTICATE = `${APP_BACKEND}/api/v${API_VER}/users/ReAuthenticateUser/`;
export const API_COMMAND_RESET_USER_PASSWORD = `${APP_BACKEND}/api/v${API_VER}/users/ResetUserPassword/`;
export const API_COMMAND_UPDATE_USER_PASSWORD = `${APP_BACKEND}/api/v${API_VER}/users/UpdateUserPassword/`;

/* API | SUBSCRIBERS */

export const API_QUERY_GET_SUBSCRIBERS = `${APP_BACKEND}/api/v${API_VER}/subscribers/GetAllSubscribers/`;
export const API_QUERY_GET_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/GetSubscriber/{id}/`;
export const API_COMMAND_ADD_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/AddSubscriber/`;
export const API_COMMAND_UPDATE_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/UpdateSubscriber/`;
export const API_COMMAND_REMOVE_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/RemoveSubscriber/`;

/* API | MAILER */

export const API_COMMAND_VERIFY_EMAIL = `${APP_BACKEND}/api/v${API_VER}/mailer/VerifyEmailAddress/`;
export const API_COMMAND_SEND_MESSAGE = `${APP_BACKEND}/api/v${API_VER}/mailer/SendMessage/`;
export const API_COMMAND_SEND_NEWSLETTER = `${APP_BACKEND}/api/v${API_VER}/mailer/SendNewsletter/`;

/* COMPONENTS CONTENT */

export const GET_NAVIGATION_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=navigation&Type=component`;
export const GET_HEADER_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=header&Type=component`;
export const GET_FOOTER_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=footer&Type=component`;
export const GET_ARTICLE_FEAT_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=articleFeatures&Type=component`;
export const GET_CONTACT_FORM_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=contactForm&Type=component`;
export const GET_COOKIES_PROMPT_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=cookiesPrompt&Type=component`;
export const GET_CLIENTS_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=clients&Type=component`;
export const GET_FEATURED_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=featured&Type=component`;
export const GET_FEATURES_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=features&Type=component`;
export const GET_NEWSLETTER_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=newsletter&Type=component`;
export const GET_RESET_PASSWORD_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=resetPassword&Type=component`;
export const GET_UPDATE_PASSWORD_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=updatePassword&Type=component`;
export const GET_SIGNIN_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=userSignin&Type=component`;
export const GET_SIGNUP_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=userSignup&Type=component`;
export const GET_SIGNOUT_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=userSignout&Type=component`;
export const GET_TESTIMONIALS_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=testimonials&Type=component`;
export const GET_UNSUBSCRIBE_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=unsubscribe&Type=component`;
export const GET_ACTIVATE_ACCOUNT_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=activateAccount&Type=component`;
export const GET_UPDATE_SUBSCRIBER_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=updateSubscriber&Type=component`;
export const GET_WRONG_PAGE_PROMPT_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=wrongPagePrompt&Type=component`;

/* DOCUMENTS CONTENT */

export const STORY_URL = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=myStory&Type=document`;
export const TERMS_URL = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=terms&Type=document`;
export const POLICY_URL = `${APP_BACKEND}/api/v${API_VER}/content/GetContent/?Name=policy&Type=document`;

/* OTHER URL */

export const IMAGE_URL = `${APP_BACKEND}/api/v${API_VER}/assets/getArticleAsset/?Id={ID}&assetName=image.jpg`;

/* PATHS */

export const ARTICLE_PATH = `/articles/?id={ID}`;
export const IMAGES_PATH = `${APP_BACKEND}/api/v${API_VER}/assets/getAsset/?BlobName=images/`;
export const ARTICLE_IMAGE_PATH = `${APP_BACKEND}/api/v${API_VER}/assets/getAsset/?BlobName=images/sections/articles/`;
export const FEATURED_IMAGE_PATH = `${APP_BACKEND}/api/v${API_VER}/assets/getAsset/?BlobName=images/sections/featured/`;
export const TESTIMONIALS_PATH = `${APP_BACKEND}/api/v${API_VER}/assets/getAsset/?BlobName=images/sections/testimonials/`;
export const ICONS_PATH = `${APP_BACKEND}/api/v${API_VER}/assets/getAsset/?BlobName=images/icons/`;
export const AVATARS_PATH = `${APP_BACKEND}/api/v${API_VER}/assets/getAsset/?BlobName=images/avatars/`;

/* ICONS */

export const MAIN_ICON = `${ICONS_PATH}main_logo.svg`;
export const MEDIUM_ICON = `${ICONS_PATH}medium_icon.svg`;

/* MESSAGES AND TEMPLATES */

export const MESSAGE_OUT_SUCCESS: string = "<p>Congratulations!</p><p>The message has been sent successfully, we will try to respond as soon as possible.</p>";
export const MESSAGE_OUT_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To send an email all fields must be filled along with acceptance of Terms of Use and Privacy Policy.</span>";
export const MESSAGE_OUT_ERROR: string = "<p>Ouch!</p><p>The message could not be sent.</p><p>{ERROR}.</p>";

export const SIGNIN_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To sign-in all fields must be filled properly.</span>";
export const SIGNUP_SUCCESS: string = "<p>Congratulations!</p><p>The account has been setup successfully, please check your e-mail and follow given instructions to make newly created account active.</p>";
export const SIGNUP_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To sign-up all fields must be filled along with acceptance of Terms of Use and Privacy Policy.</span>";

export const RESET_PASSWORD_SUCCESS: string = "<p>The password has been reset. Please check your email box and follow the instruction to setup new password.</p>";
export const RESET_PASSWORD_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To reset user password registered and verified e-mail address must be provided.</span>";

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

/* OTHER */

export const LIKES_LIMIT_FOR_ANONYM: number = 25;
export const LIKES_LIMIT_FOR_USER: number = 50;
export const LIKES_HINT_FOR_ANONYM: string = "You may give up to {LEFT_LIKES} thumbs up!";
export const LIKES_HINT_FOR_USER: string = "You may give up to {LEFT_LIKES} thumbs up!";
export const MAX_LIKES_REACHED: string = "You've reached max thumbs up :)";
export const WORDS_PER_MINUTE: number = 265;
export const RECEIVED_ERROR_MESSAGE: string = "RECEIVED_ERROR_MESSAGE";
export const NO_ERRORS: string = "NO_ERRORS";
export const ANONYMOUS_NAME: string = "Anonymous";
export const DEFAULT_NAME: string = "Unknown";
export const DEFAULT_AVATAR: string = "avatar-default-288.jpeg";
export const USER_DATA: string = "userData";

/* FORMS */

export const SIGNIN_FORM: string = "Signin Form";
export const SIGNUP_FORM: string = "Signup Form";
export const RESET_FORM: string = "Password Reset";
export const UPDATE_FORM: string = "Update Password";
export const CONTACT_FORM: string = "Contact Form";
export const NEWSLETTER: string = "Newsletter";
export const UPDATE_SUBSCRIBER: string = "Update Subscriber";
export const REMOVE_SUBSCRIBER: string = "Remove Subscriber";
