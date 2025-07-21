import { ErrorContentDto } from "Api/Models";
import { useSelector } from "react-redux";
import { ApplicationState } from "Store/Configuration";

interface ErrorBoundaryProps {
    title: string;
    subtitle: string;
    text: string;
    linkHref: string;
    linkText: string;
    footer: string;
}

const defaultContent: ErrorBoundaryProps = {
    title: "Critical Error",
    subtitle: "Something went wrong...",
    text: "Contact the site's administrator or support for assistance.",
    linkHref: "mailto:admin@tomkandula.com",
    linkText: "IT support",
    footer: "tomkandula.com",
};

export const useErrorBoundaryContent = (): ErrorBoundaryProps => {
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const filtering = (value: ErrorContentDto): boolean => {
        return value.language === language.id;
    };

    const result = language.errorBoundary.filter(filtering)[0];

    return {
        title: result?.title ?? defaultContent.title,
        subtitle: result?.subtitle ?? defaultContent.subtitle,
        text: result?.text ?? defaultContent.text,
        linkHref: result?.linkHref ?? defaultContent.linkHref,
        linkText: result?.linkText ?? defaultContent.linkText,
        footer: result?.footer ?? defaultContent.footer,
    };
};
