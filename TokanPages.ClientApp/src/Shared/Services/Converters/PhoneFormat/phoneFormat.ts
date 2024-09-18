export const formatPhoneNumber = (phoneNumberString: string): string | null => {
    const cleaned = ("" + phoneNumberString).replace(/\D/g, "");

    //Without area code (xxx xxx xxx)
    const withoutArea = cleaned.match(/^(\d{3})(\d{3})(\d{3})$/);
    if (withoutArea) {
        return `${withoutArea[1]} ${withoutArea[2]} ${withoutArea[3]}`;
    }

    //With area code (2 digits)
    const withArea2 = cleaned.match(/^(\d{2})(\d{3})(\d{3})(\d{3})$/);
    if (withArea2) {
        return `(${withArea2[1]}) ${withArea2[2]} ${withArea2[3]} ${withArea2[4]}`;
    }

    //With area code (3 digits)
    const withArea3 = cleaned.match(/^(\d{3})(\d{3})(\d{3})(\d{3})$/);
    if (withArea3) {
        return `(${withArea3[1]}) ${withArea3[2]} ${withArea3[3]} ${withArea3[4]}`;
    }

    return null;
};
