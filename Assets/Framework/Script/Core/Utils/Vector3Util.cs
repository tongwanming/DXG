using UnityEngine;

public static class Vector3Util
{
    public static Vector3 StrToVector3 (this string p_sVec3)
    {
        p_sVec3 = p_sVec3. Replace("(", ""). Replace(")", "");
        string [] tmp_sValues = p_sVec3. Trim(' '). Split(',');

        if (p_sVec3. Length <= 0)
        {
            return Vector3. zero;
        }

        if (tmp_sValues != null && tmp_sValues. Length == 3)
        {
            float tmp_fX = float. Parse(tmp_sValues [ 0 ]);
            float tmp_fY = float. Parse(tmp_sValues [ 1 ]);
            float tmp_fZ = float. Parse(tmp_sValues [ 2 ]);

            return new Vector3(tmp_fX, tmp_fY, tmp_fZ);
        }
        return Vector3. zero;
    }

    public static Vector2 StrToVector2 (this string p_sVec2)
    {
        p_sVec2 = p_sVec2. Replace("(", ""). Replace(")", "");
        string [] tmp_sValues = p_sVec2. Trim(' '). Split(',');

        if (p_sVec2. Length <= 0)
        {
            return Vector2. zero;
        }

        if (tmp_sValues != null && tmp_sValues. Length == 2)
        {
            float tmp_fX = float. Parse(tmp_sValues [ 0 ]);
            float tmp_fY = float. Parse(tmp_sValues [ 1 ]);
            return new Vector2(tmp_fX, tmp_fY);
        }
        return Vector2. zero;
    }
}
