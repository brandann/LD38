using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour
{
	private Rigidbody2D _rigidBody2d;
	private Vector2 _oldVelocity;

    public float MinBounceDifference;
    public float MaxBounceDifference;

	void Start()
	{
		_rigidBody2d = gameObject.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		_oldVelocity = _rigidBody2d.velocity;

	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag.Contains("Player"))
		{

			var thisscale = this.gameObject.transform.localScale.x;
			var otherscale = c.gameObject.transform.localScale.x;

            var diff = (thisscale / otherscale) - 1;

            // PLANET IS LARGER THAN THE PLAYER
            if (thisscale > otherscale)
			{
                if(Mathf.Abs(diff) < MaxBounceDifference)
                {
                    // do nothing
                }
                else
                {
                    print("kill player");
                    c.gameObject.SendMessage("kill");
                }
			}

            // PLANET IS SMALLER THAN THE PLAYER
            else
            {
                if(Mathf.Abs(diff) < MinBounceDifference)
                {
                    // do nothing
                }
                else
                {
                    print("Absorbed to player");
                    c.gameObject.SendMessage("absorb", this.transform.localScale.x);
                    Destroy(this.gameObject);
                }
            }
		}
		else if (c.gameObject.tag.Contains("Wall") )
		{
			//_rigidBody2d.AddForce( -1f * _rigidBody2d.velocity);
			//_rigidBody2d.AddForce(c.contacts[0].normal * 2f, ForceMode.Impulse);
			_rigidBody2d.velocity = Vector2.Reflect(_oldVelocity, c.contacts[0].normal);

		}
		else if (c.gameObject.tag.Contains("Planet"))
		{
			//_rigidBody2d.AddForce(-2.5f * _rigidBody2d.velocity);
			_rigidBody2d.velocity = Vector2.Reflect(_oldVelocity, c.contacts[0].normal);

		}

	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag.Contains("Player"))
		{
			var thisscale = this.gameObject.transform.localScale.x;
			var otherscale = c.gameObject.transform.localScale.x;
            

			if (thisscale > otherscale)
			{
				print("kill player with trigger");
				c.SendMessage("kill");
			}
			else
			{
				print("Absorbed to player");
				c.gameObject.SendMessage("absorb", this.transform.localScale.x);
				Destroy(this.gameObject);
			}
		}

	}
}