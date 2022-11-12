const API_VER = process.env.REACT_APP_API_VER;
export const APP_FRONTEND = process.env.REACT_APP_FRONTEND;
export const APP_BACKEND = process.env.REACT_APP_BACKEND;

export const API_QUERY_GET_ARTICLES = `${APP_BACKEND}/api/v${API_VER}/articles/getAllArticles/`;
export const API_QUERY_GET_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/getArticle/{id}/`;
export const API_COMMAND_ADD_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/addArticle/`;
export const API_COMMAND_UPDATE_ARTICLE_CONTENT = `${APP_BACKEND}/api/v${API_VER}/articles/updateArticleContent/`;
export const API_COMMAND_UPDATE_ARTICLE_COUNT = `${APP_BACKEND}/api/v${API_VER}/articles/updateArticleCount/`;
export const API_COMMAND_UPDATE_ARTICLE_LIKES = `${APP_BACKEND}/api/v${API_VER}/articles/updateArticleLikes/`;
export const API_COMMAND_UPDATE_ARTICLE_VISIBILITY = `${APP_BACKEND}/api/v${API_VER}/articles/updateArticleVisibility/`;
export const API_COMMAND_REMOVE_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/removeArticle/`;

export const API_QUERY_GET_USERS = `${APP_BACKEND}/api/v${API_VER}/users/getAllUsers/`;
export const API_QUERY_GET_USER = `${APP_BACKEND}/api/v${API_VER}/users/getUser/{id}/`;
export const API_COMMAND_ADD_USER = `${APP_BACKEND}/api/v${API_VER}/users/addUser/`;
export const API_COMMAND_ACTIVATE_USER = `${APP_BACKEND}/api/v${API_VER}/users/activateUser/`;
export const API_COMMAND_UPDATE_USER = `${APP_BACKEND}/api/v${API_VER}/users/updateUser/`;
export const API_COMMAND_REMOVE_USER = `${APP_BACKEND}/api/v${API_VER}/users/removeUser/`;
export const API_COMMAND_AUTHENTICATE = `${APP_BACKEND}/api/v${API_VER}/users/authenticateUser/`;
export const API_COMMAND_REAUTHENTICATE = `${APP_BACKEND}/api/v${API_VER}/users/reAuthenticateUser/`;
export const API_COMMAND_RESET_USER_PASSWORD = `${APP_BACKEND}/api/v${API_VER}/users/resetUserPassword/`;
export const API_COMMAND_UPDATE_USER_PASSWORD = `${APP_BACKEND}/api/v${API_VER}/users/updateUserPassword/`;

export const API_QUERY_GET_SUBSCRIBERS = `${APP_BACKEND}/api/v${API_VER}/subscribers/getAllSubscribers/`;
export const API_QUERY_GET_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/getSubscriber/{id}/`;
export const API_COMMAND_ADD_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/addSubscriber/`;
export const API_COMMAND_UPDATE_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/updateSubscriber/`;
export const API_COMMAND_REMOVE_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/removeSubscriber/`;

export const API_COMMAND_VERIFY_EMAIL = `${APP_BACKEND}/api/v${API_VER}/mailer/verifyEmailAddress/`;
export const API_COMMAND_SEND_MESSAGE = `${APP_BACKEND}/api/v${API_VER}/mailer/sendMessage/`;
export const API_COMMAND_SEND_NEWSLETTER = `${APP_BACKEND}/api/v${API_VER}/mailer/sendNewsletter/`;

export const API_QUERY_GET_CONTENT_MANIFEST = `${APP_BACKEND}/api/v${API_VER}/content/getContentManifest/`;
export const GET_NAVIGATION_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=navigation&Type=component`;
export const GET_HEADER_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=header&Type=component`;
export const GET_FOOTER_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=footer&Type=component`;
export const GET_ARTICLE_FEAT_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=articleFeatures&Type=component`;
export const GET_CONTACT_FORM_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=contactForm&Type=component`;
export const GET_COOKIES_PROMPT_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=cookiesPrompt&Type=component`;
export const GET_CLIENTS_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=clients&Type=component`;
export const GET_FEATURED_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=featured&Type=component`;
export const GET_FEATURES_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=features&Type=component`;
export const GET_NEWSLETTER_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=newsletter&Type=component`;
export const GET_RESET_PASSWORD_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=resetPassword&Type=component`;
export const GET_UPDATE_PASSWORD_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=updatePassword&Type=component`;
export const GET_SIGNIN_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=userSignin&Type=component`;
export const GET_SIGNUP_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=userSignup&Type=component`;
export const GET_SIGNOUT_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=userSignout&Type=component`;
export const GET_TESTIMONIALS_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=testimonials&Type=component`;
export const GET_UNSUBSCRIBE_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=unsubscribe&Type=component`;
export const GET_ACTIVATE_ACCOUNT_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=activateAccount&Type=component`;
export const GET_UPDATE_SUBSCRIBER_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=updateSubscriber&Type=component`;
export const GET_WRONG_PAGE_PROMPT_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=wrongPagePrompt&Type=component`;
export const GET_ACCOUNT_CONTENT = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=account&Type=component`;

export const STORY_URL = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=myStory&Type=document`;
export const TERMS_URL = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=terms&Type=document`;
export const POLICY_URL = `${APP_BACKEND}/api/v${API_VER}/content/getContent/?name=policy&Type=document`;

export const IMAGE_URL = `${APP_BACKEND}/api/v${API_VER}/assets/getArticleAsset/?Id={ID}&assetName=image.jpg`;

export const ARTICLE_PATH = `/articles/?id={ID}`;
export const IMAGES_PATH = `${APP_BACKEND}/api/v${API_VER}/assets/getAsset/?blobName=images/`;
export const ARTICLE_IMAGE_PATH = `${APP_BACKEND}/api/v${API_VER}/assets/getAsset/?blobName=images/sections/articles/`;
export const FEATURED_IMAGE_PATH = `${APP_BACKEND}/api/v${API_VER}/assets/getAsset/?blobName=images/sections/featured/`;
export const TESTIMONIALS_PATH = `${APP_BACKEND}/api/v${API_VER}/assets/getAsset/?blobName=images/sections/testimonials/`;
export const ICONS_PATH = `${APP_BACKEND}/api/v${API_VER}/assets/getAsset/?blobName=images/icons/`;
export const AVATARS_PATH = `${APP_BACKEND}/api/v${API_VER}/assets/getAsset/?blobName=images/avatars/`;

export const MAIN_ICON = `${ICONS_PATH}main_logo.svg`;
export const MEDIUM_ICON = `${ICONS_PATH}medium_icon.svg`;

export const LIKES_LIMIT_FOR_ANONYM: number = 25;
export const LIKES_LIMIT_FOR_USER: number = 50;
export const WORDS_PER_MINUTE: number = 265;

export const SELECTED_LANGUAGE: string = "SELECTED_LANGUAGE";
export const RECEIVED_ERROR_MESSAGE: string = "RECEIVED_ERROR_MESSAGE";
export const NO_ERRORS: string = "NO_ERRORS";
export const USER_DATA: string = "USER_DATA";


// --------------------------- TODO: move out below code from constants --------------------------- //
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
