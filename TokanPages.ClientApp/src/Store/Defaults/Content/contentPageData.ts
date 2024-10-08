import { OperationStatus } from "../../../Shared/enums";
import { ContentPageDataState } from "../../../Store/States";

export const ContentPageData: ContentPageDataState = {
    status: OperationStatus.notStarted,
    isLoading: false,
    languageId: undefined,
    components: {
        account: {
            content: {
                language: "",
                sectionAccessDenied: {
                    accessDeniedCaption: "",
                    accessDeniedPrompt: "",
                    homeButtonText: "",
                },
                sectionAccountInformation: {
                    caption: "",
                    labelUserId: "",
                    labelEmailStatus: {
                        label: "",
                        negative: "",
                        positive: "",
                    },
                    labelUserAlias: "",
                    labelFirstName: "",
                    labelLastName: "",
                    labelEmail: "",
                    labelShortBio: "",
                    labelUserAvatar: "",
                    updateButtonText: "",
                    uploadAvatarButtonText: "",
                },
                sectionAccountPassword: {
                    caption: "",
                    labelOldPassword: "",
                    labelNewPassword: "",
                    labelConfirmPassword: "",
                    updateButtonText: "",
                },
                sectionAccountDeactivation: {
                    caption: "",
                    warningText: "",
                    deactivateButtonText: "",
                },
                sectionAccountRemoval: {
                    caption: "",
                    warningText: "",
                    deleteButtonText: "",
                },
            },
        },
        activateAccount: {
            content: {
                language: "",
                onVerifying: {
                    text1: "",
                    text2: "",
                    type: "",
                    caption: "",
                    button: "",
                },
                onProcessing: {
                    text1: "",
                    text2: "",
                    type: "",
                    caption: "",
                    button: "",
                },
                onSuccess: {
                    noBusinessLock: {
                        text1: "",
                        text2: "",
                    },
                    businessLock: {
                        text1: "",
                        text2: "",
                    },
                    type: "",
                    caption: "",
                    button: "",
                },
                onError: {
                    text1: "",
                    text2: "",
                    type: "",
                    caption: "",
                    button: "",
                },
            },
        },
        article: {
            content: {
                language: "",
                button: "",
                textReadCount: "",
                textFirstName: "",
                textSurname: "",
                textRegistered: "",
                textLanguage: "",
                textReadTime: "",
                textPublished: "",
                textUpdated: "",
                textWritten: "",
                textAbout: "",
            },
        },
        articleFeatures: {
            content: {
                language: "",
                title: "",
                description: "",
                text1: "",
                text2: "",
                action: {
                    text: "",
                    href: "",
                },
                image1: "",
                image2: "",
                image3: "",
                image4: "",
            },
        },
        businessForm: {
            content: {
                language: "",
                caption: "",
                buttonText: "",
                companyLabel: "",
                firstNameLabel: "",
                lastNameLabel: "",
                emailLabel: "",
                phoneLabel: "",
                techLabel: "",
                techItems: [],
                description: {
                    label: "",
                    multiline: false,
                    rows: 0,
                    required: false,
                },
                pricing: {
                    caption: "",
                    disclaimer: "",
                    services: [],
                },
            },
        },
        clients: {
            content: {
                language: "",
                caption: "",
                images: [],
            },
        },
        contactForm: {
            content: {
                language: "",
                caption: "",
                text: "",
                button: "",
                consent: "",
                labelFirstName: "",
                labelLastName: "",
                labelEmail: "",
                labelSubject: "",
                labelMessage: "",
            },
        },
        cookiesPrompt: {
            content: {
                language: "",
                caption: "",
                text: "",
                button: "",
                days: 0,
            },
        },
        document: {
            content: {
                language: "",
                items: [],
            },
        },
        featured: {
            content: {
                language: "",
                caption: "",
                text: "",
                title1: "",
                subtitle1: "",
                link1: "",
                image1: "",
                title2: "",
                subtitle2: "",
                link2: "",
                image2: "",
                title3: "",
                subtitle3: "",
                link3: "",
                image3: "",
            },
        },
        footer: {
            content: {
                language: "",
                terms: {
                    text: "",
                    href: "",
                },
                policy: {
                    text: "",
                    href: "",
                },
                copyright: "",
                reserved: "",
                icons: [],
            },
        },
        header: {
            content: {
                language: "",
                photo: "",
                caption: "",
                subtitle: "",
                description: "",
                action: {
                    text: "",
                    href: "",
                },
                resume: {
                    text: "",
                    href: "",
                },
            },
        },
        navigation: {
            content: {
                language: "",
                logo: "",
                userInfo: {
                    textUserAlias: "",
                    textRegistered: "",
                    textRoles: "",
                    textPermissions: "",
                    textButton: "",
                },
                menu: {
                    image: "",
                    items: [],
                },
            },
        },
        newsletter: {
            content: {
                language: "",
                caption: "",
                text: "",
                button: "",
                labelEmail: "",
            },
        },
        newsletterRemove: {
            content: {
                language: "",
                contentPre: {
                    caption: "",
                    text1: "",
                    text2: "",
                    text3: "",
                    button: "",
                },
                contentPost: {
                    caption: "",
                    text1: "",
                    text2: "",
                    text3: "",
                    button: "",
                },
            },
        },
        newsletterUpdate: {
            content: {
                language: "",
                caption: "",
                button: "",
                labelEmail: "",
            },
        },
        resetPassword: {
            content: {
                language: "",
                caption: "",
                button: "",
                labelEmail: "",
            },
        },
        technologies: {
            content: {
                language: "",
                caption: "",
                header: "",
                title1: "",
                text1: "",
                title2: "",
                text2: "",
                title3: "",
                text3: "",
                title4: "",
                text4: "",
            },
        },
        templates: {
            content: {
                language: "",
                forms: {
                    textSigning: "",
                    textSignup: "",
                    textPasswordReset: "",
                    textUpdatePassword: "",
                    textBusinessForm: "",
                    textContactForm: "",
                    textCheckoutForm: "",
                    textNewsletter: "",
                    textAccountSettings: "",
                },
                templates: {
                    application: {
                        unexpectedStatus: "",
                        unexpectedError: "",
                        validationError: "",
                        nullError: "",
                    },
                    password: {
                        emailInvalid: "",
                        nameInvalid: "",
                        surnameInvalid: "",
                        passwordInvalid: "",
                        missingTerms: "",
                        missingChar: "",
                        missingNumber: "",
                        missingLargeLetter: "",
                        missingSmallLetter: "",
                        resetSuccess: "",
                        resetWarning: "",
                        updateSuccess: "",
                        updateWarning: "",
                    },
                    articles: {
                        success: "",
                        warning: "",
                        error: "",
                        likesHintAnonym: "",
                        likesHintUser: "",
                        maxLikesReached: "",
                    },
                    newsletter: {
                        success: "",
                        warning: "",
                        generalError: "",
                        removalError: "",
                    },
                    messageOut: {
                        success: "",
                        warning: "",
                        error: "",
                    },
                    user: {
                        deactivation: "",
                        removal: "",
                        updateSuccess: "",
                        updateWarning: "",
                        emailVerification: "",
                        signingWarning: "",
                        signupSuccess: "",
                        signupWarning: "",
                    },
                    payments: {
                        checkoutWarning: "",
                    },
                },
            },
        },
        testimonials: {
            content: {
                language: "",
                caption: "",
                subtitle: "",
                photo1: "",
                name1: "",
                occupation1: "",
                text1: "",
                photo2: "",
                name2: "",
                occupation2: "",
                text2: "",
                photo3: "",
                name3: "",
                occupation3: "",
                text3: "",
            },
        },
        updatePassword: {
            content: {
                language: "",
                caption: "",
                button: "",
                labelNewPassword: "",
                labelVerifyPassword: "",
            },
        },
        userSignin: {
            content: {
                language: "",
                caption: "",
                button: "",
                link1: "",
                link2: "",
                labelEmail: "",
                labelPassword: "",
            },
        },
        userSignout: {
            content: {
                language: "",
                caption: "",
                onProcessing: "",
                onFinish: "",
                buttonText: "",
            },
        },
        userSignup: {
            content: {
                language: "",
                caption: "",
                button: "",
                link: "",
                warning: "",
                consent: "",
                labelFirstName: "",
                labelLastName: "",
                labelEmail: "",
                labelPassword: "",
            },
        },
    },
};
