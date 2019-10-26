namespace BooksReader.Dictionaries.Messages
{
    public partial class MessageStrings
    {
        public class BookPricesMessages
        {
            public static string Prefix = "";

            public static string PriceBookIdIsNotForEditedBook = Prefix + "PRICE_BOOK_ID_IS_NOT_FOR_EDITED_BOOK";
            public static string PriceShouldBeGreaterThanZero = Prefix + "PRICE_SHOULD_BE_GREATER_THAN_ZERO";
            public static string SelectedCurrencyDoesNotExists = Prefix + "SELECTED_CURRENCY_DOES_NOT_EXISTS";
            public static string PriceItemWithSelectedIdNOtExists = Prefix + "PRICE_ITEM_WITH_SELECTED_ID_NOT_EXISTS";
            public static string PriceWithTheSameCurrencyExists = Prefix + "PRICE_WITH_THE_SAME_CURRENCY_EXISTS";
            public static string BookDoesNotExists = Prefix + "BOOK_FOR_THE_PRICE_DOES_NOT_EXISTS";
            public static string NotAnOwnerOrAdmin = Prefix + "YOU_ARE_NOT_AN_OWNER_OR_ADMIN";
        }
    }
}
