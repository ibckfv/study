using Homework;

namespace HomeworkTests;

public class HomeworkTasksTests
{
    [Fact]
    public void Sent_Normalized_Words_Which_Have_Preffix_Return_Uni()
    {
        var stringsArray = new []{ "university", "universe", "uniform"};
        var expectedPreffix = "uni";
        
        var result = HomeworkTasks.LongestCommonPrefix(stringsArray);
        
        Assert.Equal(expectedPreffix, result);
    }

    [Fact]
    public void Sent_Words_Which_Havent_Preffix_Return_Empty_String()
    {
        var stringsArray = new[] { "надо ходить на учёбу", "ненадо ходить", "ходить" };
        var expectedPreffix = "";

        var result = HomeworkTasks.LongestCommonPrefix(stringsArray);

        Assert.Equal(expectedPreffix, result);
    }

    [Fact]
    public void Sent_Words_When_One_Of_Them_Have_Only_One_Symbol_Thrown_Invalid_Operation_Exception()
    {
        var errorArray = new[] { "apple", "a", "pple" };

        Assert.Throws<InvalidOperationException>(() => HomeworkTasks.LongestCommonPrefix(errorArray));
    }

    [Fact]
    public void Sent_Normalized_Array_Return_Upscaled_For_Two_Array()
    {
        var booleansArray = new bool [2, 3]
        {
            {true, true, true},
            {false, false, false},
        };
        var upscaleCount = 2;
        var expectedArray = new int [4, 6]
        {
            {1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1},
            {0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0}
        };

        var result = HomeworkTasks.UpscaleImage(booleansArray, upscaleCount);

        Assert.Equal(expectedArray, result);
    }

    [Fact]
    public void Sent_Array_Wihg_Only_One_Row_Return_Upscaled_Array_With_Three_Rows()
    {
        var booleansArray = new bool[1, 3]
        {
            {true, false, true}
        };
        var upscaleCount = 3;
        var expectedArray = new int[3, 9]
        {
            {1, 1, 1, 0, 0, 0, 1, 1, 1},
            {1, 1, 1, 0, 0, 0, 1, 1, 1},
            {1, 1, 1, 0, 0, 0, 1, 1, 1}
        };

        var result = HomeworkTasks.UpscaleImage(booleansArray, upscaleCount);

        Assert.Equal(expectedArray, result);
    }

    [Fact]
    public void Sent_Square_Array_Thrown_Exception()
    {
        var errorArray = new bool[2, 2]
        {
            {true, false},
            {false, true},
        };
        var upscaleCount = 2;

        Assert.Throws<Exception>(() => HomeworkTasks.UpscaleImage(errorArray, upscaleCount));
    }

    [Fact]
    public void Sent_Normalized_Words_With_Normalized_Pattern_Return_True()
    {
        var str = "abc abc cba cba";
        var pattern = "aabb";

        var result = HomeworkTasks.WordPattern(pattern, str);

        Assert.True(result);
    }

    [Fact]
    public void Sent_Normalized_Words_With_Uncorrect_Pattern_Return_False()
    {
        var str = "abc cba cba cba";
        var pattern = "ab";

        var result = HomeworkTasks.WordPattern(pattern, str);

        Assert.False(result);
    }

    [Fact]
    public void Sent_Normalized_Words_With_Only_One_Repeating_Character_In_Pattern_Thrown_Exception()
    {
        var str = "abc cba";
        var errorPattern = "aa";

        Assert.Throws<Exception>(() => HomeworkTasks.WordPattern(errorPattern, str));
    }
}