import * as React from "react";
import { BusinessFormView } from "./View/businessFormView";

export interface BusinessFormProps {
    pt?: number;
    pb?: number;
    hasCaption?: boolean;
    hasIcon?: boolean;
    hasShadow?: boolean;
    background?: React.CSSProperties;
}

//TODO: add logic
export const BusinessForm = (props: BusinessFormProps): JSX.Element => {
    return(
        <BusinessFormView
            isLoading={false}
            caption="Business Inquiry"
            text=""
            progress={false}
            buttonText="Submit"
            keyHandler={() => {}}
            formHandler={() => {}}
            buttonHandler={() => {}}
            companyText=""
            companyLabel="Company Name"
            firstNameText=""
            firstNameLabel="First Name"
            lastNameText=""
            lastNameLabel="Last Name"
            emailText=""
            emailLabel="Business Email"
            phoneText=""
            phoneLabel="Business Phone"
            techHeader="Which technologies are you interested in?"
            techItems={[
                { value: ".NET/C#", key: 0 },
                { value: "JavaScript/TypeScript", key: 1 },
                { value: "React/ReactNative", key: 2 },
                { value: "Material UI", key: 3 },
                { value: "MS SQL/T-SQL", key: 4 },
                { value: "Entity Framework Core", key: 5 },
                { value: "VPS w/Docker images", key: 6 },
                { value: "Microsoft Azure Cloud", key: 7 },
            ]}
            techLabel=""
            description={{
                text: "",
                label: "Project description",
                multiline: true,
                rows: 12,
                required: true,
            }}
            pricing={{
                caption: "Which services are you interested in?",
                programing: "Software development (web/mobile/backend).",
                programmingPrice: "<b>30 USD/hour</b>",
                hosting: "Hosting and maintaining the solution on a dedicated VPS.",
                hostingPrice: "<b>60 USD/month</b>",
                support: "Additional <i>ad hoc</i> support after roll-out.",
                supportPrice: "<b>15 USD/hour</b>",
                info: "Please note: the above values are indicative only without 23% of VAT, and do not constitute a commercial offer.",
            }}
            pt={props.pt}
            pb={props.pb}
            background={props.background}
            hasIcon={props.hasIcon}
            hasCaption={props.hasCaption}
            hasShadow={props.hasShadow}
        />
    );
};
