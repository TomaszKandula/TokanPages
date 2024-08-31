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
            firstNameLabel="Name"
            lastNameText=""
            lastNameLabel="Surname"
            emailText=""
            emailLabel="Business Email"
            phoneText=""
            phoneLabel="Business Phone"
            techHeader="Required key technologies"
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
                web: {
                    header: "Describe your project that will involve building frontend with or without backend. You may also specify another UI framework. However, please do note that backend can be done only with C# language.",
                    text: "",
                    label: "Required description",
                    multiline: true,
                    rows: 10,
                    required: true,
                },
                mobile: {
                    header: "Describe mobile application you want me to build.",
                    text: "",
                    label: "Required description",
                    multiline: true,
                    rows: 10,
                    required: true,
                },
                info: {
                    header: "You may specify project tenure and some other requirements like specific CI/CD etc.",
                    text: "",
                    label: "Optional additional information",
                    multiline: true,
                    rows: 6,
                }
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
