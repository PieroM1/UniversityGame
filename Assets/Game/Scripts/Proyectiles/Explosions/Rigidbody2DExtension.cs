using UnityEngine;

public static class Rigidbody2DExtension
{
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float exposionRadius)
    {
        //direccion de la fuerza a aplicar
        Vector3 forceVector = body.transform.position - explosionPosition;
        //variable para controlar la fuerza a palciar dependiendo de la distancia del objeto al punto de explosion
        float wearoff = 1 - (forceVector.magnitude/exposionRadius);
        body.AddForce(forceVector.normalized * explosionForce * wearoff); 
    }

}
