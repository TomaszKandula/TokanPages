import { OperationStatus } from "../../../Shared/enums";
import { ContentPageDataState } from "../../../Store/States";

export const ContentPageData: ContentPageDataState = {
    status: OperationStatus.notStarted,
    isLoading: false,
    languageId: undefined,
    components: {
        accountUserNotes: {
            language: "",
            caption: "",
            description: "",
            listLabel: "",
            noteLabel: "",
            buttons: {
                removeText: "",
                clearText: "",
                saveText: ""
            }
        },
        accountSettings: {
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
        accountActivate: {
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
        article: {
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
        articleFeatures: {
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
        businessForm: {
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
        clients: {
            language: "",
            caption: "",
            images: [],
        },
        contactForm: {
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
        cookiesPrompt: {
            language: "",
            caption: "",
            text: "",
            detail: "",
            loading: [],
            options: {
                enabled: false,
                necessaryLabel: "",
                statisticsLabel: "",
                marketingLabel: "",
                personalizationLabel: ""
            },
            buttons: {
                acceptButton: {
                    label: "",
                    enabled: false
                },
                manageButton: {
                    label: "",
                    enabled: false
                },
                closeButton: {
                    label: "",
                    enabled: false
                }
            },
            days: 0,
        },
        featured: {
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
        footer: {
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
        header: {
            language: "",
            photo: {
                w360: "",
                w720: "",
                w1440: "",
                w2880: "",
            },
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
        navigation: {
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
        newsletter: {
            language: "",
            caption: "",
            text: "",
            button: "",
            labelEmail: "",
        },
        newsletterRemove: {
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
        newsletterUpdate: {
            language: "",
            caption: "",
            button: "",
            labelEmail: "",
        },
        passwordReset: {
            language: "",
            caption: "",
            button: "",
            labelEmail: "",
        },
        technologies: {
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
        templates: {
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
        testimonials: {
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
        passwordUpdate: {
            language: "",
            caption: "",
            button: "",
            labelNewPassword: "",
            labelVerifyPassword: "",
        },
        accountUserSignin: {
            language: "",
            caption: "",
            button: "",
            link1: {
                text: "",
                href: ""
            },
            link2: {
                text: "",
                href: ""
            },
            labelEmail: "",
            labelPassword: "",
        },
        accountUserSignout: {
            language: "",
            caption: "",
            onProcessing: "",
            onFinish: "",
            buttonText: "",
        },
        accountUserSignup: {
            language: "",
            caption: "",
            button: "",
            link: {
                text: "",
                href: ""
            },
            warning: "",
            consent: "",
            labelFirstName: "",
            labelLastName: "",
            labelEmail: "",
            labelPassword: "",
        },
        policy: {
            language: "",
            items: [],
        },
        terms: {
            language: "",
            items: [],
        },
        about: {
            language: "",
            items: [],
        },
        story: {
            language: "",
            items: [],
        },
        showcase: {
            language: "",
            items: [],
        },
        bicycle: {
            language: "",
            items: [],
        },
        electronics: {
            language: "",
            items: [],
        },
        football: {
            language: "",
            items: [],
        },
        guitar: {
            language: "",
            items: [],
        },
        photography: {
            language: "",
            items: [],
        },
    },
};
